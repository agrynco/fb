using System.Net;
using FluentAssertions;
using Web.API.IntegrationTests.Core;
using Web.API.Models.Users.Authenticate;
using Web.API.Models.Users.Register;
using Xunit;

namespace Web.API.IntegrationTests;

public class UsersControllerTests
{
    [Fact]
    public async Task Register_Should_Create_User_Successfully()
    {
        using var apiTestFixture = new ApiTestFixture();

        await apiTestFixture.PerformPost("/users/register", new RegisterUserInput
        {
            Email = "xtes@email.com",
            Password = "password",
            Username = "XUsername",
            FirstName = "XFirstName",
            LastName = "XLastName"
        });

        Assert.True(true);
    }

    [Fact]
    public async Task Authenticate_Family_Budget_Should_Be_Ok()
    {
        using var apiTestFixture = new ApiTestFixture();

        var actual = (await apiTestFixture.PerformPost<AuthenticateUserOutput>("/users/authenticate",
            new AuthenticateUserInput
            {
                Password = "PH44szAV",
                Username = "family.budget"
            }))!;

        actual.Id.Should().Be(2);
        actual.Email.Should().Be("family.budget.2023@gmail.com");
        actual.FirstName.Should().Be("Family");
        actual.LastName.Should().Be("Budget");
        actual.JwtToken.Should().NotBeNullOrEmpty();
        actual.RefreshToken.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Authenticate_Family_Budget_Should_Fail_With_Wrong_Password()
    {
        using var apiTestFixture = new ApiTestFixture();

        try
        {
            await apiTestFixture.PerformPost<AuthenticateUserOutput>("/users/authenticate",
                new AuthenticateUserInput
                {
                    Password = "wrong_password",
                    Username = "family.budget"
                });
        }
        catch (ApiTestFixtureResponseException ex)
        {
            ex.ResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}