using Web.API.IntegrationTests.Core;
using Web.API.Models.Users.Authenticate;

namespace Web.API.IntegrationTests;

public static class TestUtils
{
    public static ApiTestFixture BuildAuthorizedAPITestFixture()
    {
        return new ApiTestFixture(new AuthenticateUserInput
        {
            Username = "agrynco",
            Password = "PH44szAV"
        });
    }
}