namespace Services.FamilyMembers.Update;

using AutoMapper;
using Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FamilyMembersUpdateMessage, FamilyMember>();
    }
}