namespace Services.FamilyMembers.GetByOwner;

using DAL.Abstract.FamilyMembers;
using Serilog;
using Services.Core;

public class FamilyMembersGetByOwnerRequestHandler(ILogger logger, IFamilyMembersRepository familyMembersRepository)
    : RequestHandler<FamilyMembersGetByOwnerRequest, object>(logger)
{
    protected override async Task<object> DoOnHandle(FamilyMembersGetByOwnerRequest request)
    {
        return await familyMembersRepository.GetAll(request.AuthorizedUserId, request.LoadOptions);
    }
}