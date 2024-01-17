namespace Web.API.Models;

[Serializable]
public class RequestError
{
    private readonly List<RequestErrorItem> _errorItems = new();

    public RequestError(params RequestErrorItem[] errors)
    {
        foreach (RequestErrorItem errorItem in errors)
        {
            AddError(errorItem);
        }
    }

    public List<RequestErrorItem> Errors => _errorItems;

    public void AddError(RequestErrorItem errorItem)
    {
        _errorItems.Add(errorItem);
    }
}