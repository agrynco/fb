namespace Services.Accounts.CardAccounts.Create;

using AutoMapper;
using Domain.Finances;
using Services.Accounts.CashAccounts.Create;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CardAccountsCreateRequest, CardAccount>();
    }
}