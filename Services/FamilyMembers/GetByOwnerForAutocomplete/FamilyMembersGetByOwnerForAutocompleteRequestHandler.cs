namespace Services.FamilyMembers.GetByOwnerForAutocomplete;

using DAL.Abstract.FamilyMembers;
using Serilog;
using Services.Core;

public class
    FamilyMembersGetByOwnerForAutocompleteRequestHandler(
        ILogger logger,
        IFamilyMembersRepository familyMembersRepository) 
    : RequestHandler<FamilyMembersGetByOwnerForAutocompleteRequest,
        FamilyMemberDto[]>(logger)
{
    protected override async Task<FamilyMemberDto[]> DoOnHandle(FamilyMembersGetByOwnerForAutocompleteRequest request)
    {
        return await familyMembersRepository.GetAll(request.AuthorizedUserId);
    }
}