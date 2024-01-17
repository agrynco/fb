namespace DAL.EF.Repositories.Banks;

using DAL.Abstract.Banks;
using DAL.EF.Core;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Domain.Finances;
using Microsoft.EntityFrameworkCore;

public class BanksRepository(
        ApplicationDbContext applicationDbContext)
    : BaseRepository<Bank>(applicationDbContext), IBanksRepository
{
    public async Task<object> GetAll(int ownerId, DataSourceLoadOptions loadOptions)
    {
        return await Task.FromResult(DataSourceLoader.Load(EfRepository.Get(e => e.OwnerId == ownerId), loadOptions));
    }

    public async Task<Bank?> GetById(int id)
    {
        return await EfRepository.GetByIdAsync(id);
    }

    public async Task<int[]> GetOwnerIds(int[] ids)
    {
        return await EfRepository.Get(e => ids.Contains(e.Id)).Select(e => e.OwnerId).Distinct().ToArrayAsync();
    }

    public async Task<int> Add(Bank bank)
    {
        return (await EfRepository.AddAsync(bank)).Id;
    }

    public async Task Update(Bank bank)
    {
        await EfRepository.UpdateAsync(bank);
    }

    public async Task Delete(int[] ids)
    {
        await EfRepository.DeleteAsync(ids);
    }
}