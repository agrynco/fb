namespace DAL.EF.Repositories.FamilyMembers;

using AutoMapper;
using DAL.Abstract.FamilyMembers;
using Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FamilyMember, FamilyMemberDto>();
    }
}