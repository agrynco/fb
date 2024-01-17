namespace Services.Accounts.CardAccounts.Update;

using AutoMapper;
using Domain.Finances;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CardAccountsUpdateMessage, CardAccount>();
    }
}