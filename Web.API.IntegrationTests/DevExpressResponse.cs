namespace Web.API.IntegrationTests;

/// <summary>
/// Represents a response from DevExpress with a collection of data items.
/// </summary>
/// <typeparam name="TDataItem">The type of data item in the response.</typeparam>
public class DevExpressResponse<TDataItem>
{
    public TDataItem[] Data { get; set; } = Array.Empty<TDataItem>();
}