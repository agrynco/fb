namespace Services.Accounts.BankAccounts.GetById;

using AutoMapper;
using Domain.Finances;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BankAccount, BankAccountsGetByIdResponse>();
    }
}