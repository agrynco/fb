namespace Services.Accounts.CashAccounts.Update;

using AutoMapper;
using Domain.Finances;
using Services.Accounts.CashAccounts.GetById;
using Services.Accounts.Update;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CashAccountsUpdateMessage, CashAccount>();
        CreateMap<CashAccountsUpdateMessage, Account>()
            .ForMember(d => d.OwnerId, opt => opt.MapFrom(s => s.AuthorizedUserId));
    }
}