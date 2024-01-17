using Domain.Finances.Transactions;

namespace DAL.Abstract.TransactionCategories;

public interface ITransactionCategoriesRepository
{
    Task<TransactionCategory[]> Get(int ownerId, int? parentId);
    Task<int?> FindByName(string name, int ownerId, int? parentId);
    Task<bool> IsExists(int ownerId, int id);
    Task<int> Add(TransactionCategory transactionCategory);
    Task Update(TransactionCategory transactionCategory);
    Task Delete(int id);
    Task<TransactionCategory?> GetForEdit(int id);
    Task<TransactionCategory?> GetById(int id);
}