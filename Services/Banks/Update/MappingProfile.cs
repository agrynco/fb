namespace Services.Banks.Update;

using AutoMapper;
using Domain.Finances;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BanksUpdateMessage, Bank>().
            ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.AuthorizedUserId));
        
    }
}