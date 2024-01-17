namespace Services.FamilyMembers.GetByOwner;

using DevExtreme.AspNet.Mvc;
using SlimMessageBus;

public record FamilyMembersGetByOwnerRequest : IRequest<object>
{
    public int AuthorizedUserId { get; init; }
    public DataSourceLoadOptions LoadOptions { get; init; } = null!;
}