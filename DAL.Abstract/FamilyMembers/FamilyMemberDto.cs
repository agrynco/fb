namespace DAL.Abstract.FamilyMembers;

public record FamilyMemberDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int OwnerId { get; set; }
}