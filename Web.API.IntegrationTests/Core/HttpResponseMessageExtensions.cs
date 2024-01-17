namespace Web.API.IntegrationTests.Core;

public static class HttpResponseMessageExtensions
{
    public static void EnsureOk(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiTestFixtureResponseException(response);
        }
    }
}