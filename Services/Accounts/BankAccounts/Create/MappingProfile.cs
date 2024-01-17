namespace Services.Accounts.BankAccounts.Create;

using AutoMapper;
using Domain.Finances;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BankAccountsCreateRequest, BankAccount>();
    }
}