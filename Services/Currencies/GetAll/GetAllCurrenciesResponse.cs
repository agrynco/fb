namespace Services.Currencies.GetAll;

public class GetAllCurrenciesResponse(GetAllCurrenciesItem[] items)
{
    public GetAllCurrenciesItem[] Items { get; } = items;
}