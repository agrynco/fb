namespace DAL.EF.Repositories.Users.Accounts.CashAccounts;

using DAL.Abstract.Accounts.CashAccounts;
using DAL.Abstract.Currencies;
using DAL.EF.Core;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Domain.Finances;
using Microsoft.EntityFrameworkCore;

public class CashAccountsRepository : BaseRepository<CashAccount>, ICashAccountsRepository
{
    public CashAccountsRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

    public object GetByOwner(int ownerId, DataSourceLoadOptions loadOptions)
    {
        return DataSourceLoader.Load(EfRepository.Get(e => e.OwnerId == ownerId)
            .Include(e => e.Currency)
            .Select(e => new CashAccountsGetByOwnerItem
            {
                Balance = e.Balance,
                Closed = e.Closed,
                Currency = new CurrencyDto
                {
                    Id = e.CurrencyId,
                    IsoCode = e.Currency.IsoCode,
                    Name = e.Currency.Name,
                    SymbolOrAbbrev = e.Currency.SymbolOrAbbrev
                },
                Description = e.Description,
                Id = e.Id,
                Name = e.Name,
                Verified = e.Verified
            }), loadOptions);
    }

    public async Task<CashAccount> GetById(int id)
    {
        return await EfRepository.Get(e => e.Id == id).Include(e => e.Currency).SingleAsync();
    }

    public async Task<CashAccount> GetForUpdate(int id)
    {
        return await EfRepository.Get(e => e.Id == id).SingleAsync();
    }

    public async Task Update(CashAccount account) { await EfRepository.UpdateAsync(account); }

    public async Task<int> Add(CashAccount account) { return (await EfRepository.AddAsync(account)).Id; }
}