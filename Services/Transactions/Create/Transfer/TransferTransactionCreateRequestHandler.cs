namespace Services.Transactions.Create.Transfer;

using Common;
using DAL.Abstract;
using DAL.Abstract.Accounts;
using DAL.Abstract.Core;
using DAL.Abstract.Transaction;
using Domain.Finances;
using Domain.Finances.Transactions;
using Serilog;
using Services.Core;

public class TransferTransactionCreateRequestHandler(
    ILogger logger,
    IAccountsRepository accountsRepository,
    IUnitOfWork unitOfWork,
    ITransactionsRepository transactionsRepository,
    ITransactionCorrelationsRepository transactionCorrelationsRepository,
    IClock clock)
    : RequestHandler<TransferTransactionCreateRequest, int>(logger)
{
    protected override async Task<int> DoOnHandle(TransferTransactionCreateRequest request)
    {
        return await new Func<Task<int>>(async () =>
        {
            Account sourceAccount = await accountsRepository.GetById(request.SourceAccountId);
            Account destinationAccount = await accountsRepository.GetById(request.DestinationAccountId);

            if (sourceAccount.CurrencyId != destinationAccount.CurrencyId && request.ExchangeRate == null)
            {
                throw new ExchangeRateNotProvidedException(request);
            }

            decimal exchangeRate = request.ExchangeRate ?? 1;

            DateTime transactionDateTime = request.TransactionDateTime ?? clock.UtcNow;

            var outcomeTransaction = new Transaction
            {
                ActorId = request.ActorId,
                Amount = request.Amount * -1,
                CategoryId = request.CategoryId,
                Description = request.Description,
                AccountId = request.SourceAccountId,
                TransactionDateTime = transactionDateTime,
                FamilyMemberId = request.FamilyMemberId,
                Confirmed = request.Confirmed
            };

            decimal destinationAmount = request.Amount * exchangeRate;

            var incomeTransaction = new Transaction
            {
                ActorId = request.ActorId,
                Amount = destinationAmount,
                CategoryId = request.CategoryId,
                Description = request.Description,
                AccountId = request.DestinationAccountId,
                TransactionDateTime = transactionDateTime,
                FamilyMemberId = request.FamilyMemberId,
                Confirmed = request.Confirmed
            };

            sourceAccount.Balance -= request.Amount;
            destinationAccount.Balance += destinationAmount;

            await transactionsRepository.Add(outcomeTransaction);
            await transactionsRepository.Add(incomeTransaction);

            int transactionCorrelationId = await transactionCorrelationsRepository.Add(new TransactionCorrelation
            {
                IncomeTransactionId = incomeTransaction.Id,
                OutcomeTransactionId = outcomeTransaction.Id,
                ExchangeRate = exchangeRate
            });

            incomeTransaction.TransactionCorrelationId = transactionCorrelationId;
            await transactionsRepository.Update(incomeTransaction);
            outcomeTransaction.TransactionCorrelationId = transactionCorrelationId;
            await transactionsRepository.Update(outcomeTransaction);

            sourceAccount.Verified = false;
            destinationAccount.Verified = false;
            await accountsRepository.Update(sourceAccount);
            await accountsRepository.Update(destinationAccount);

            return outcomeTransaction.Id;
        }).ExecuteInTransactionAsync(unitOfWork, request);
    }
}