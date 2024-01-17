namespace DAL.EF.Repositories.Users.Accounts;

using AutoMapper;
using DAL.Abstract.Accounts;
using DAL.Abstract.Currencies;
using DAL.Abstract.Transaction;
using Domain.Finances;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Account, AccountDto>();
        CreateMap<Currency, CurrencyDto>();
        CreateMap<Account, AccountsGetByOwnerItem>();
    }
}