namespace Web.API.Models.Currencies.GetAll;

public class CurrenciesGetAllItemOutput
{
    public int Id { get; set; }
    public required string IsoCode { get; set; } = default!;
    public required string Name { get; set; } = default!;
    public required string SymbolOrAbbrev { get; set; } = default!;
}