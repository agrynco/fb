using AutoMapper;
using DAL.Abstract.Accounts;
using Domain.Finances;

namespace Services;

using DAL.Abstract.Currencies;

public class CommonMappingProfile : Profile
{
    public CommonMappingProfile()
    {
        CreateMap<Currency, CurrencyListItemDto>();
        CreateMap<Currency, CurrencyDto>();
    }
}