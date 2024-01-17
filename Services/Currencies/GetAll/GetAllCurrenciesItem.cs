namespace Services.Currencies.GetAll;

public class GetAllCurrenciesItem
{
    public int Id { get; set; }
    public required string IsoCode { get; set; }
    public required string Name { get; set; }
    public required string SymbolOrAbbrev { get; set; }
}