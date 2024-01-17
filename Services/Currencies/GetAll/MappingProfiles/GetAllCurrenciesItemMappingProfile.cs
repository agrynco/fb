using AutoMapper;
using Domain.Finances;

namespace Services.Currencies.GetAll.MappingProfiles;

public class GetAllCurrenciesItemMappingProfile : Profile
{
    public GetAllCurrenciesItemMappingProfile()
    {
        CreateMap<Currency, GetAllCurrenciesItem>();
    }
}