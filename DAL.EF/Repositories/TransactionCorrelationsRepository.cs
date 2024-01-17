using DAL.Abstract;
using DAL.EF.Core;
using Domain.Finances.Transactions;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories;

public class TransactionCorrelationsRepository : BaseRepository<TransactionCorrelation>,
    ITransactionCorrelationsRepository
{
    public TransactionCorrelationsRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public async Task<TransactionCorrelation?> GetByTransaction(int transactionId)
    {
        return await EfRepository.Get(e => e.IncomeTransactionId == transactionId || e.OutcomeTransactionId == transactionId)
            .SingleOrDefaultAsync();
    }

    public async Task<int> Add(TransactionCorrelation transactionCorrelation)
    {
        return (await EfRepository.AddAsync(transactionCorrelation)).Id;
    }

    public async Task<TransactionCorrelation?> GetById(int id)
    {
        return await EfRepository.GetByIdAsync(id);
    }

    public async Task Delete(int id)
    {
        await EfRepository.DeleteAsync(id);
    }
}