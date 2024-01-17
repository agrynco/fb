using FluentAssertions;
using Web.API.IntegrationTests.Core;
using Xunit;

namespace Web.API.IntegrationTests;

public class SystemControllerTests
{
    [Fact]
    public async Task Version_Returns_Valid_Result()
    {
        using var apiTestFixture = new ApiTestFixture();

        string actual = await apiTestFixture.PerformGet("/system/version");

        actual.Should().Be("1.0.0.0");
    }
}