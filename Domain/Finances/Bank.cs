namespace Domain.Finances;

public class Bank : NamedEntity
{
    public int OwnerId { get; set; }
    public string? Description { get; set; }
}