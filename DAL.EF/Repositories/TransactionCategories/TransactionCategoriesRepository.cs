using DAL.Abstract.TransactionCategories;
using DAL.EF.Core;
using Domain.Finances.Transactions;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories.TransactionCategories;

public class TransactionCategoriesRepository(ApplicationDbContext applicationDbContext)
    : BaseRepository<TransactionCategory>(applicationDbContext), ITransactionCategoriesRepository
{
    public async Task<TransactionCategory[]> Get(int ownerId, int? parentId)
    {
        return await EfRepository.Get(e => e.OwnerId == ownerId && (parentId == null || e.ParentId == parentId))
            .Include(e => e.Owner)
            .ToArrayAsync();
    }

    public async Task<int?> FindByName(string name, int ownerId, int? parentId)
    {
        TransactionCategory? transactionCategory = await EfRepository
            .Get(e => e.Name == name && e.OwnerId == ownerId && e.ParentId == parentId)
            .SingleOrDefaultAsync();
        return transactionCategory?.Id;
    }

    public async Task<bool> IsExists(int ownerId, int id)
    {
        return await EfRepository.Get(e => e.Id == id && e.OwnerId == ownerId).AnyAsync();
    }

    public async Task<int> Add(TransactionCategory transactionCategory)
    {
        return (await EfRepository.AddAsync(transactionCategory)).Id;
    }

    public async Task Update(TransactionCategory transactionCategory)
    {
        await EfRepository.UpdateAsync(transactionCategory);
    }

    public async Task Delete(int id)
    {
        await EfRepository.DeleteAsync(id);
    }

    public async Task<TransactionCategory?> GetForEdit(int id)
    {
        return await EfRepository.GetByIdAsync(id);
    }

    public async Task<TransactionCategory?> GetById(int id)
    {
        return await EfRepository.Get(e => e.Id == id).SingleOrDefaultAsync();
    }
}