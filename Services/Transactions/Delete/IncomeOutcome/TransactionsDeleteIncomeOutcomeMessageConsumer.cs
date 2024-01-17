namespace Services.Transactions.Delete.IncomeOutcome;

using DAL.Abstract;
using DAL.Abstract.Accounts;
using DAL.Abstract.Core;
using DAL.Abstract.Transaction;
using Domain.Finances;
using Domain.Finances.Transactions;
using Serilog;
using Services.Core;

public class TransactionsDeleteIncomeOutcomeMessageConsumer(
    ILogger logger,
    IUnitOfWork unitOfWork,
    ITransactionsRepository transactionsRepository,
    IAccountsRepository accountsRepository,
    IGeoLocationPositionsRepository geoLocationPositionsRepository)
    : MessageConsumer<TransactionsDeleteIncomeOutcomeMessage>(logger)
{
    protected override async Task DoOnHandle(TransactionsDeleteIncomeOutcomeMessage message)
    {
        Transaction transaction = (await transactionsRepository.GetById(message.Id))!;

        if (transaction.ActorId != message.ActorId)
        {
            throw new WrongTransactionOwnerException(transaction.Id, message.ActorId);
        }

        await new Func<Task>(async () =>
        {
            Account targetAccount = await accountsRepository.GetById(transaction.AccountId);
            targetAccount.Balance -= transaction.Amount;

            await accountsRepository.Update(targetAccount);

            if (transaction.GeoLocationPositionId != null)
            {
                await geoLocationPositionsRepository.Delete(transaction.GeoLocationPositionId.Value);
            }

            await transactionsRepository.Delete(message.Id);
        }).ExecuteInTransactionAsync(unitOfWork, message);
    }
}