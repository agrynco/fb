namespace DAL.EF.Repositories.Users.Accounts;

using AutoMapper;
using DAL.Abstract.Accounts;
using DAL.Abstract.Accounts.CashAccounts;
using DAL.Abstract.Currencies;
using DAL.EF.Core;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Domain.Finances;
using Microsoft.EntityFrameworkCore;

public class AccountsRepository(
    ApplicationDbContext applicationDbContext,
    IMapper mapper)
    : BaseRepository<Account>(applicationDbContext), IAccountsRepository
{
    public object GetByOwner(int ownerId, DataSourceLoadOptions loadOptions)
    {
        return DataSourceLoader.Load(EfRepository.Get(e => e.OwnerId == ownerId)
            .Include(e => e.Currency)
            .Select(e => new AccountsGetByOwnerItem
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
                Type = e.Type
            }), loadOptions);
    }

    public async Task<Account> GetById(int id)
    {
        return await EfRepository.Get(e => e.Id == id).Include(e => e.Currency).SingleAsync();
    }

    public async Task<AccountDto[]> GetByIds(int[] ids)
    {
        return await EfRepository
            .Get(e => ids.Contains(e.Id))
            .Include(e => e.Currency)
            .Select(e => mapper.Map<AccountDto>(e))
            .ToArrayAsync();
    }

    public async Task Update(Account account) { await EfRepository.UpdateAsync(account); }

    public async Task<Account> GetForUpdate(int id) { return await EfRepository.Get(e => e.Id == id).SingleAsync(); }

    public async Task Delete(int id) { await EfRepository.DeleteAsync(id); }

    public async Task<int> Add(Account account) { return (await EfRepository.AddAsync(account)).Id; }

    public async Task<int[]> GetOwnerIds(int[] ids)
    {
        return await (from e in EfRepository.GetAll()
            where ids.Contains(e.Id)
            select e.OwnerId).Distinct().ToArrayAsync();
    }

    public async Task<AccountDto[]> GetByOwner(int ownerId)
    {
        return await EfRepository.Get(e => e.OwnerId == ownerId).Select(e => new AccountDto
        {
            Id = e.Id,
            Name = e.Name,
            Closed = e.Closed,
            Currency = new CurrencyDto
            {
                Id = e.CurrencyId,
                IsoCode = e.Currency.IsoCode,
                Name = e.Currency.Name,
                SymbolOrAbbrev = e.Currency.SymbolOrAbbrev
            }
        }).ToArrayAsync();
    }

    public async Task<object> GetWideByOwnerId(int ownerId, DataSourceLoadOptions loadOptions)
    {
        var query = from a in EfRepository.Get(e => e.OwnerId == ownerId).Include(e => e.Currency)
            join bankAccount in EfRepository.DbContext.BankAccounts.Include(e => e.Bank) on a.Id equals bankAccount.Id
                into bankAccounts
            from bankAccount in bankAccounts.DefaultIfEmpty()
            join cardAccount in EfRepository.DbContext.CardAccounts.Include(e => e.Bank) on a.Id equals cardAccount.Id
                into cardAccounts
            from cardAccount in cardAccounts.DefaultIfEmpty()
            select new WideAccountDto
            {
                Id = a.Id,
                Type = a.Type,
                Name = a.Name,
                Closed = a.Closed,
                OwnerId = a.OwnerId,
                Balance = a.Balance,
                Description = a.Description,
                Verified = a.Verified,
                Currency = new CurrencyDto
                {
                    Id = a.CurrencyId,
                    IsoCode = a.Currency.IsoCode,
                    Name = a.Currency.Name,
                    SymbolOrAbbrev = a.Currency.SymbolOrAbbrev
                },
                Bank = bankAccount == null
                    ? cardAccount == null
                        ? null
                        : new BankDto
                        {
                            Id = cardAccount.BankId,
                            Name = cardAccount.Bank.Name
                        }
                    : new BankDto
                    {
                        Id = bankAccount.BankId,
                        Name = bankAccount.Bank.Name
                    },
                BankAccountInfo = bankAccount == null
                    ? null
                    : new BankAccountIfo
                    {
                        IBAN = bankAccount.IBAN
                    },
                CardAccountInfo = cardAccount == null
                    ? null
                    : new CardAccountInfo
                    {
                        CardNumber = cardAccount.CardNumber
                    }
            };

        return await Task.FromResult(DataSourceLoader.Load(query, loadOptions));
    }

    public async Task<WideAccountDto> GetWideById(int id)
    {
        var query = from a in EfRepository.Get(e => e.Id == id).Include(e => e.Currency)
            join bankAccount in EfRepository.DbContext.BankAccounts.Include(e => e.Bank) on a.Id equals bankAccount.Id
                into bankAccounts
            from bankAccount in bankAccounts.DefaultIfEmpty()
            join cardAccount in EfRepository.DbContext.CardAccounts.Include(e => e.Bank) on a.Id equals cardAccount.Id
                into cardAccounts
            from cardAccount in cardAccounts.DefaultIfEmpty()
            select BuildWideAccountDto(a, bankAccount, cardAccount);

        return await query.SingleAsync();
    }

    private static WideAccountDto BuildWideAccountDto(Account a, BankAccount bankAccount, CardAccount cardAccount)
    {
        return new WideAccountDto
        {
            Id = a.Id,
            Type = a.Type,
            Name = a.Name,
            Closed = a.Closed,
            OwnerId = a.OwnerId,
            Balance = a.Balance,
            Description = a.Description,
            Currency = new CurrencyDto
            {
                Id = a.CurrencyId,
                IsoCode = a.Currency.IsoCode,
                Name = a.Currency.Name,
                SymbolOrAbbrev = a.Currency.SymbolOrAbbrev
            },
            Bank = bankAccount == null
                ? cardAccount == null
                    ? null
                    : new BankDto
                    {
                        Id = cardAccount.BankId,
                        Name = cardAccount.Bank.Name
                    }
                : new BankDto
                {
                    Id = bankAccount.BankId,
                    Name = bankAccount.Bank.Name
                },
            BankAccountInfo = bankAccount == null
                ? null
                : new BankAccountIfo
                {
                    IBAN = bankAccount.IBAN
                },
            CardAccountInfo = cardAccount == null
                ? null
                : new CardAccountInfo
                {
                    CardNumber = cardAccount.CardNumber
                }
        };
    }
}