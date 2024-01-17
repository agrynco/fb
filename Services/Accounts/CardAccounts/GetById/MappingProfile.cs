namespace Services.Accounts.CardAccounts.GetById;

using AutoMapper;
using Domain.Finances;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CardAccount, CardAccountsGetByIdResponse>();
    }
}