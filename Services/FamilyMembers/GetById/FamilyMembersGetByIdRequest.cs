namespace Services.FamilyMembers.GetById;

using DAL.Abstract.FamilyMembers;
using SlimMessageBus;

public record FamilyMembersGetByIdRequest : IRequest<FamilyMemberDto>
{
    public int AuthorizedUserId { get; init; }
    public int Id { get; init; }
}