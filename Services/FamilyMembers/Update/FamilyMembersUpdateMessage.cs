namespace Services.FamilyMembers.Update;

public record FamilyMembersUpdateMessage
{
    public int AuthorizedUserId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
}