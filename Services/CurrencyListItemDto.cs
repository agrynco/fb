namespace Services;

public class CurrencyListItemDto
{
    public int Id { get; set; }
    public required string IsoCode { get; set; } = default!;
    public required string Name { get; set; } = default!;
}