namespace Services.FamilyMembers.Create;

using DAL.Abstract.FamilyMembers;
using Domain;
using Serilog;
using Services.Core;

public class FamilyMembersCreateRequestHandler(ILogger logger, IFamilyMembersRepository familyMembersRepository)
    : RequestHandler<FamilyMembersCreateRequest, int>(logger)
{
    protected override async Task<int> DoOnHandle(FamilyMembersCreateRequest request)
    {
        var familyMember = new FamilyMember
        {
            Name = request.Name,
            OwnerId = request.AuthorizedUserId
        };

        return await familyMembersRepository.Add(familyMember);
    }
}