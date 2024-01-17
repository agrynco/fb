namespace Services.Transactions.Create.IncomeOutcome;

using AutoMapper;
using Common;
using DAL.Abstract.Accounts;
using DAL.Abstract.Core;
using DAL.Abstract.Transaction;
using Domain.Finances;
using Domain.Finances.Transactions;
using Serilog;
using Services.Core;

public class TransactionsCreateIncomeOutcomeRequestHandler(
    ILogger logger,
    ITransactionsRepository transactionsRepository,
    IAccountsRepository accountsRepository,
    IUnitOfWork unitOfWork,
    IClock clock,
    IMapper mapper)
    : RequestHandler<TransactionsCreateIncomeOutcomeRequest, int>(logger)
{
    protected override async Task<int> DoOnHandle(TransactionsCreateIncomeOutcomeRequest request)
    {
        return await new Func<Task<int>>(async () =>
        {
            DateTime transactionDateTime = request.TransactionDateTime ?? clock.UtcNow;

            var incomeOrOutcomeTransaction = mapper.Map<Transaction>(request);
            incomeOrOutcomeTransaction.TransactionDateTime = transactionDateTime;
            incomeOrOutcomeTransaction.AccountId = request.TargetAccountId;

            await transactionsRepository.Add(incomeOrOutcomeTransaction);

            Account account = await accountsRepository.GetById(request.TargetAccountId);
            account.Verified = false;
            account.Balance += request.Amount;

            await accountsRepository.Update(account);

            return incomeOrOutcomeTransaction.Id;
        }).ExecuteInTransactionAsync(unitOfWork, request);
    }
}