namespace DAL.Abstract.Transaction;

using Domain.Finances;
using Domain.Finances.Transactions;

public interface ITransactionsRepository
{
    Task<bool> AreAnyTransactionWithAccounts(int[] accountIds);
    Task<int> Add(Transaction transaction);
    Task Update(Transaction transaction);
    Task<Transaction?> GetById(int id);
    Task Delete(int id);
    Task<int[]> GetAccountIdsByTransactionIds(int[] transactionIds);
    Task<object> Get(TransactionsDataSourceLoadOptions loadOptions, int actorId, int languageId);
    Task<int[]> GetCategoryIds(int actorId);

    /// <summary>
    /// Checks whether any of the <see cref="Transaction"/> IDs in the given array are locked.
    /// <see cref="Transaction"/> is locked if any related <see cref="Account"/> is <see cref="Account.Closed"/>
    /// </summary>
    /// <param name="transactionIds">An array of transaction IDs to check.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a boolean value indicating whether any of the <see cref="Transaction"/> IDs are locked.
    /// </returns>
    Task<bool> ContainsLocked(int[] transactionIds);
}