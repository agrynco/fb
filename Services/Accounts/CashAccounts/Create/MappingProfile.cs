namespace Services.Accounts.CashAccounts.Create;

using AutoMapper;
using Domain.Finances;
using Services.Accounts.CardAccounts.Update;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CashAccountsCreateRequest, CashAccount>();
    }
}