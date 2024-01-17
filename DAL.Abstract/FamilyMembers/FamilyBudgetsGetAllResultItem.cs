namespace DAL.Abstract.FamilyMembers;

public record FamilyBudgetsGetAllResultItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int OwnerId { get; set; }
}