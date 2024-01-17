namespace Services.FamilyMembers.Update;

using AutoMapper;
using DAL.Abstract.FamilyMembers;
using Domain;
using Serilog;
using Services.Core;

public class FamilyMembersUpdateMessageConsumer(
    ILogger logger,
    IFamilyMembersRepository familyMembersRepository,
    IMapper mapper)
    : MessageConsumer<FamilyMembersUpdateMessage>(logger)
{
    protected override async Task DoOnHandle(FamilyMembersUpdateMessage message)
    {
        FamilyMember familyMember = await familyMembersRepository.GetById(message.Id); 
        
        if (familyMember.OwnerId != message.AuthorizedUserId)
        {
            throw new WrongFamilyMemberOwnerException(new []{message.Id}, message.AuthorizedUserId);
        }

        mapper.Map(message, familyMember);
        
        await familyMembersRepository.Update(familyMember);
    }
}