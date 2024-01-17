namespace Web.API.IntegrationTests.Core;

[Serializable]
public class ApiTestFixtureResponseException : Exception
{
    public ApiTestFixtureResponseException(HttpResponseMessage responseMessage, Exception? internalException = null)
        : base($"HttpStatus is not OK. The actual value is {responseMessage.StatusCode}", internalException)
    {
        ResponseMessage = responseMessage;
    }

    public HttpResponseMessage ResponseMessage { get; }
}