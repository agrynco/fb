using AutoMapper;
using Domain;

namespace Services.Users.Authenticate.MappingProfiles;

public class GenerateRefreshTokenResultMappingProfile : Profile
{
    public GenerateRefreshTokenResultMappingProfile()
    {
        CreateMap<GenerateRefreshTokenResult, RefreshToken>();
    }
}