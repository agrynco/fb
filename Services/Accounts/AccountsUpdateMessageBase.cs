namespace Services.Accounts;

public record AccountsUpdateMessageBase
{
    public int AuthorizedUserId { get; set; }
    public bool Closed { get; set; }
    public int CurrencyId { get; set; }
    public string? Description { get; set; }
    public int Id { get; set; }

    public required string Name { get; set; } = default!;
}