namespace Domain.Finances;

public class Country : NamedEntity
{
    public List<Currency> Currencies { get; private set; } = new();
}