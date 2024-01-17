namespace Services.Banks.Create;

using AutoMapper;
using Domain.Finances;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BanksCreateRequest, Bank>();
    }
}