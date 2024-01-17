namespace Services.Banks.GetById;

using AutoMapper;
using Domain.Finances;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Bank, BanksGetByIdResponse>();
    }
}