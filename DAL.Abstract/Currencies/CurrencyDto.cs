namespace DAL.Abstract.Currencies;

public class CurrencyDto
{
    public int Id { get; set; }
    public string IsoCode { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string SymbolOrAbbrev { get; set; } = default!;
}