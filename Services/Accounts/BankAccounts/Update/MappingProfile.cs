namespace Services.Accounts.BankAccounts.Update;

using AutoMapper;
using Domain.Finances;
using Services.Accounts.CardAccounts.Update;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BankAccountsUpdateMessage, BankAccount>();
    }
}