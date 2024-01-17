namespace Domain;

public class FamilyMember : NamedEntity
{
    public User Owner { get; set; }
    public int OwnerId { get; set; }
}