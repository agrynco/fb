namespace Services.FamilyMembers.Create;

public record FamilyMembersCreateRequest : ICreateRequest
{
    public string Name { get; set; }
    public int AuthorizedUserId { get; set; }
}