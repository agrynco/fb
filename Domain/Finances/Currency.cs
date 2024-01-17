namespace Domain.Finances;

public class Currency : NamedEntity
{
    public List<Country> Countries { get; private set; } = new();
    public required string IsoCode { get; set; } = default!;
    public required string SymbolOrAbbrev { get; set; } = default!;
}