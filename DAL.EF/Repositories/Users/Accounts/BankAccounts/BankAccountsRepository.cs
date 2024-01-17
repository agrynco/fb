namespace DAL.EF.Repositories.Users.Accounts.BankAccounts;

using DAL.Abstract.Accounts.BankAccounts;
using DAL.Abstract.Accounts.CashAccounts;
using DAL.Abstract.Currencies;
using DAL.EF.Core;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Domain.Finances;
using Microsoft.EntityFrameworkCore;

public class BankAccountsRepository : BaseRepository<BankAccount>, IBankAccountsRepository
{
    public BankAccountsRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

    public object GetByOwner(int ownerId, DataSourceLoadOptions loadOptions)
    {
        return DataSourceLoader.Load(EfRepository.Get(e => e.OwnerId == ownerId)
            .Include(e => e.Currency)
            .Include(e => e.Bank)
            .Select(e => new BankAccountsGetByOwnerItem
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
                Bank = new BankDto
                {
                    Id = e.BankId,
                    Name = e.Bank.Name
                },
                IBan = e.IBAN,
                Verified = e.Verified
            }), loadOptions);
    }

    public async Task<BankAccount> GetById(int id)
    {
        return await EfRepository.Get(e => e.Id == id)
            .Include(e => e.Currency)
            .Include(e => e.Bank)
            .SingleAsync();
    }

    public async Task Update(BankAccount account) { await EfRepository.UpdateAsync(account); }

    public async Task<int> Add(BankAccount account) { return (await EfRepository.AddAsync(account)).Id; }
}