namespace DAL.EF.Repositories.Users.Accounts.CardAccounts;

using DAL.Abstract.Accounts.CashAccounts;
using DAL.Abstract.Currencies;
using DAL.EF.Core;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Domain.Finances;
using Microsoft.EntityFrameworkCore;

public class CardAccountsRepository : BaseRepository<CardAccount>, ICardAccountsRepository
{
    public CardAccountsRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

    public object GetByOwner(int ownerId, DataSourceLoadOptions loadOptions)
    {
        return DataSourceLoader.Load(EfRepository.Get(e => e.OwnerId == ownerId)
            .Include(e => e.Currency)
            .Include(e => e.Bank)
            .Select(e => new CardAccountsGetByOwnerItem
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
                CardNumber = e.CardNumber,
                Verified = e.Verified
            }), loadOptions);
    }

    public async Task<CardAccount> GetById(int id)
    {
        return await EfRepository.Get(e => e.Id == id)
            .Include(e => e.Currency)
            .Include(e => e.Bank)
            .SingleAsync();
    }

    public async Task Update(CardAccount account) { await EfRepository.UpdateAsync(account); }

    public async Task<int> Add(CardAccount account) { return (await EfRepository.AddAsync(account)).Id; }
}