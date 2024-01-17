namespace Domain.Finances;

public class Account : NamedEntity
{
    public decimal Balance { get; set; }
    public bool Closed { get; set; }
    public Currency Currency { get; set; } = null!;
    public int CurrencyId { get; set; }
    public string? Description { get; set; }
    public User Owner { get; set; } = null!;
    public int OwnerId { get; set; }
    public AccountType Type { get; set; }

    public bool Verified { get; set; }
}