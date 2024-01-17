using AutoMapper;
using Domain.Finances;

namespace Services.Accounts.GetById;

public class AccountsGetByIdResponseMappingProfile : Profile
{
    public AccountsGetByIdResponseMappingProfile()
    {
        CreateMap<Account, AccountsGetByIdResponse>();
    }
}