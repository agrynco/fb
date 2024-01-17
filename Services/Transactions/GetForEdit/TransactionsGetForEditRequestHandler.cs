namespace Services.Transactions.GetForCreateOrEdit;

using DAL.Abstract;
using DAL.Abstract.Transaction;
using Domain.Finances.Transactions;
using Serilog;
using Services.Core;

public class TransactionsGetForEditRequestHandler(ILogger logger,
        ITransactionsRepository transactionsRepository,
        ITransactionCorrelationsRepository transactionCorrelationsRepository)
    : RequestHandler<TransactionsGetForEditRequest, TransactionsGetForEditResponse>(logger)
{
    protected override async Task<TransactionsGetForEditResponse> DoOnHandle(
        TransactionsGetForEditRequest request)
    {
        Transaction transaction = (await transactionsRepository.GetById(request.Id))!;

        if (transaction.ActorId != request.AuthorizedUserId)
        {
            throw new WrongTransactionOwnerException(transaction.Id, request.AuthorizedUserId);
        }
        
        Transaction? incomeTransaction = null;
        Transaction? outcomeTransaction = null;
        
        TransactionCorrelation? transactionCorrelation =
            await transactionCorrelationsRepository.GetByTransaction(request.Id);

        if (transactionCorrelation != null)
        {
            incomeTransaction = await transactionsRepository.GetById(transactionCorrelation!.IncomeTransactionId);
            outcomeTransaction = await transactionsRepository.GetById(transactionCorrelation!.OutcomeTransactionId);
        }

        return new TransactionsGetForEditResponse
        {
            Amount = transaction.Amount,
            CategoryId = transaction.CategoryId,
            Description = transaction.Description,
            DestinationAccountId = incomeTransaction?.AccountId,
            ExchangeRate = transactionCorrelation?.ExchangeRate,
            SourceAccountId = outcomeTransaction?.AccountId ?? transaction.AccountId,
            TransactionDateTime = transaction.TransactionDateTime,
            FamilyMemberId = transaction.FamilyMemberId,
            Confirmed = transaction.Confirmed
        };
    }
}