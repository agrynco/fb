namespace Web.API.IntegrationTests.Banks;

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
    public async Task Post_Should_Be_OK()
    {
        using ApiTestFixture apiTestFixture = TestUtils.BuildAuthorizedAPITestFixture();

        var addBankModel = new AddOrUpdateBankModel
        {
            Name = "Test bank",
            Description = "Test description"
        };

        int actual = await apiTestFixture.PerformPost<int>("banks", addBankModel);

        actual.Should().Be(5);

        using IServiceScope scope = apiTestFixture.Services.CreateScope();
        await using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        Bank? bank = await appContext.Banks.FindAsync(actual);
        
        bank.Should().NotBeNull();
        bank!.Name.Should().Be(addBankModel.Name);
        bank.Description.Should().Be(addBankModel.Description);
    }
}