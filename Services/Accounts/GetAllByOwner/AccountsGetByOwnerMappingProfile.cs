using AutoMapper;
using DAL.Abstract.Accounts;
using Domain.Finances;

namespace Services.Accounts.GetAll;

using DAL.Abstract.Accounts.BankAccounts;
using DAL.Abstract.Accounts.CashAccounts;

public class AccountsGetByOwnerMappingProfile : Profile
{
    public AccountsGetByOwnerMappingProfile()
    {
        CreateMap<CardAccount, CardAccountsGetByOwnerItem>();
        CreateMap<Bank, BankDto>();
        CreateMap<BankAccount, BankAccountsGetByOwnerItem>();
        CreateMap<Account, AccountsGetByOwnerItem>();
        CreateMap<CashAccount, AccountsGetByOwnerItem>();
        CreateMap<CardAccount, AccountsGetByOwnerItem>();
        CreateMap<BankAccount, AccountsGetByOwnerItem>();
    }
}