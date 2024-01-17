namespace Web.API.IntegrationTests;

using System.Net;
using DAL.Abstract.Accounts;
using FluentAssertions;
using Web.API.IntegrationTests.Core;
using Web.API.Models.Accounts;
using Web.API.Models.Users.Authenticate;
using Xunit;

public class AccountsControllerTests
{
    [Fact]
    public async Task GetMyAccounts()
    {
        using var apiTestFixture = new ApiTestFixture(new AuthenticateUserInput
        {
            Username = "agrynco",
            Password = "PH44szAV"
        });

        var actual = (await apiTestFixture.PerformGet<DevExpressResponse<AccountsGetByOwnerItem>>("accounts/my"))!;

        actual.Data.Length.Should().Be(9);
    }

    [Fact]
    public async Task Update_CurrencyNotChanged()
    {
        using var apiTestFixture = new ApiTestFixture(new AuthenticateUserInput
        {
            Username = "agrynco",
            Password = "PH44szAV"
        });

        HttpResponseMessage response = await apiTestFixture.PerformUpdate("accounts/cash/1", new CashAccountsCreateOrUpdateModel
        {
            Name = "Changed Name",
            CurrencyId = 1
        });

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Create()
    {
        using var apiTestFixture = new ApiTestFixture(new AuthenticateUserInput
        {
            Username = "agrynco",
            Password = "PH44szAV"
        });

        (await apiTestFixture.PerformPost<int>("accounts/cash", new CashAccountsCreateOrUpdateModel
        {
            Name = "New created account",
            CurrencyId = 1
        })).Should().NotBe(0);
    }
    
    [Fact]
    public async Task GetMyAccountsWide()
    {
        using var apiTestFixture = new ApiTestFixture(new AuthenticateUserInput
        {
            Username = "agrynco",
            Password = "PH44szAV"
        });

        var actual = (await apiTestFixture.PerformGet<DevExpressResponse<WideAccountDto>>("accounts/my/wide"))!;

        actual.Data.Length.Should().Be(9);
    }
}