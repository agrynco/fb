namespace Web.API.IntegrationTests.Core;

using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Web.API.Models.Users.Authenticate;
using Xunit;

public class ApiTestFixture : IClassFixture<TestingWebAppFactory>, IDisposable
{
    private readonly TestingWebAppFactory _testingWebAppFactory;
    private bool _disposed;

    public ApiTestFixture(AuthenticateUserInput authenticateUserInput) : this()
    {
        Authenticate(authenticateUserInput).Wait();
    }

    public ApiTestFixture()
    {
        _testingWebAppFactory = new TestingWebAppFactory();
        HttpClient = _testingWebAppFactory.CreateClient();
        HttpClient.DefaultRequestHeaders.Add("X-Forwarded-For", "127.0.0.1");
    }

    public HttpClient HttpClient { get; }

    public IServiceProvider Services => _testingWebAppFactory.Services;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task Authenticate(AuthenticateUserInput input)
    {
        string json = JsonConvert.SerializeObject(input);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await HttpClient.PostAsync("/users/authenticate", httpContent);

        response.EnsureSuccessStatusCode();

        string responseString = await response.Content.ReadAsStringAsync();
        var authenticateUserOutput = JsonConvert.DeserializeObject<AuthenticateUserOutput>(responseString)!;

        HttpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", authenticateUserOutput.JwtToken);
    }

    public async Task<HttpResponseMessage> PerformUpdate(string path,
        object input)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, path);

        string updateJson = JsonConvert.SerializeObject(input);
        var httpContentUpdate = new StringContent(updateJson, Encoding.UTF8, "application/json");
        request.Content = httpContentUpdate;

        return await HttpClient.SendAsync(request);
    }

    public async Task<string> PerformGet(string path)
    {
        HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, path));
        response.EnsureOk();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<TResult?> PerformGet<TResult>(string path)
    {
        string responseAsString = await PerformGet(path);

        var result = JsonConvert.DeserializeObject<TResult>(responseAsString);

        return result;
    }

    public async Task<string> PerformPost(string path, object input)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, path);

        string json = JsonConvert.SerializeObject(input);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        request.Content = httpContent;

        HttpResponseMessage response = await HttpClient.SendAsync(request);
        response.EnsureOk();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<TResult?> PerformPost<TResult>(string path, object input)
    {
        var result = JsonConvert.DeserializeObject<TResult>(await PerformPost(path, input));

        return result;
    }

    public async Task PerformDelete(string path)
    {
        HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, path));
        response.EnsureOk();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            HttpClient.Dispose();

            _testingWebAppFactory.Dispose();
        }

        _disposed = true;
    }
}