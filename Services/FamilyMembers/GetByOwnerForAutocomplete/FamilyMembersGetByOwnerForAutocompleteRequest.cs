namespace Services.FamilyMembers.GetByOwnerForAutocomplete;

using DAL.Abstract.FamilyMembers;
using SlimMessageBus;

public record FamilyMembersGetByOwnerForAutocompleteRequest : IRequest<FamilyMemberDto[]>
{
    public int? AuthorizedUserId { get; set; }
}