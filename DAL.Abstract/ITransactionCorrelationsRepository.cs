using Domain.Finances.Transactions;

namespace DAL.Abstract;

public interface ITransactionCorrelationsRepository
{
    Task<TransactionCorrelation?> GetByTransaction(int transactionId);
    Task<int> Add(TransactionCorrelation transactionCorrelation);
    Task<TransactionCorrelation?> GetById(int id);
    Task Delete(int id);
}