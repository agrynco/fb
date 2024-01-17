namespace Web.API.IntegrationTests.Banks;

using System.Net;
using DAL.EF;
using Domain.Finances;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Web.API.IntegrationTests.Core;
using Web.API.Models.Banks;
using Xunit;

public partial class BanksController_Tests
{
    [Fact]
    public async Task Update_Should_Be_OK()
    {
        using ApiTestFixture apiTestFixture = TestUtils.BuildAuthorizedAPITestFixture();

        var updateBankModel = new AddOrUpdateBankModel
        {
            Name = "Test bank",
            Description = "Test description"
        };

        HttpResponseMessage actual = await apiTestFixture.PerformUpdate("banks/1", updateBankModel);

        actual.StatusCode.Should().Be(HttpStatusCode.OK);

        using IServiceScope scope = apiTestFixture.Services.CreateScope();
        await using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        Bank? bank = await appContext.Banks.FindAsync(1);
        
        bank.Should().NotBeNull();
        bank!.Name.Should().Be(updateBankModel.Name);
        bank.Description.Should().Be(updateBankModel.Description);
    }
}