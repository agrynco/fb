namespace DAL.Abstract.Accounts;

using DAL.Abstract.Currencies;

public record AccountDto
{
    public decimal Balance { get; set; }
    public bool Closed { get; set; }
    public CurrencyDto Currency { get; set; } = default!;
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}