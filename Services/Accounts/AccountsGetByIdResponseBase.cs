namespace Services.Accounts;

using DAL.Abstract.Accounts;
using DAL.Abstract.Currencies;

public record AccountsGetByIdResponseBase
{
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public bool Closed { get; set; }
    public CurrencyDto Currency { get; set; } = null!;
    public string? Description { get; set; }
    public required string Name { get; set; } = default!;
}