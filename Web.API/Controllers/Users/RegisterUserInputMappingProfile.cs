using AutoMapper;
using Services.Users.Register;
using Web.API.Models.Users.Register;

namespace Web.API.Controllers.Users;

public class RegisterUserInputMappingProfile : Profile
{
    public RegisterUserInputMappingProfile()
    {
        CreateMap<RegisterUserInput, RegisterUserMessage>();
    }
}