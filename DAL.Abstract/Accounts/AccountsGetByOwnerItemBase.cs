namespace DAL.Abstract.Accounts;

using DAL.Abstract.Currencies;

public record AccountsGetByOwnerItemBase
{
    public decimal Balance { get; set; }
    public bool Closed { get; set; }
    public CurrencyDto Currency { get; set; } = default!;
    public string? Description { get; set; }
    public int Id { get; set; }
    public required string Name { get; set; } = default!;
}