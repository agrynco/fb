namespace Services.FamilyMembers.GetById;

using AutoMapper;
using DAL.Abstract.FamilyMembers;
using DAL.Abstract.Transaction;
using Domain;
using Serilog;
using Services.Core;

public class FamilyMembersGetByIdRequestHandler(
    ILogger logger,
    IFamilyMembersRepository familyMembersRepository,
    IMapper mapper)
    : RequestHandler<FamilyMembersGetByIdRequest, FamilyMemberDto>(logger)
{
    protected override async Task<FamilyMemberDto> DoOnHandle(FamilyMembersGetByIdRequest request)
    {
        FamilyMember familyMember = (await familyMembersRepository.GetById(request.Id))!;
        
        if (familyMember.OwnerId != request.AuthorizedUserId)
        {
            throw new WrongFamilyMemberOwnerException(new []{request.Id}, request.AuthorizedUserId);
        }
        
        return mapper.Map<FamilyMemberDto>(familyMember);
    }
}