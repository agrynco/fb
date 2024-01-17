namespace Services.Accounts.CashAccounts.GetById;

using AutoMapper;
using Domain.Finances;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CashAccount, CashAccountsGetByIdResponse>();
    }
}