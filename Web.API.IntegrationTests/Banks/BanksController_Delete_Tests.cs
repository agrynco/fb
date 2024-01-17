namespace Web.API.IntegrationTests.Banks;

using DAL.EF;
using Domain.Finances;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Banks;
using Services.Core;
using Web.API.IntegrationTests.Core;
using Xunit;

public partial class BanksController_Tests
{
    [Fact]
    public async Task Delete_Should_Be_OK()
    {
        // Arrange
        using ApiTestFixture apiTestFixture = TestUtils.BuildAuthorizedAPITestFixture();

        using IServiceScope scope = apiTestFixture.Services.CreateScope();
        await using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var newBank = (await appContext.Banks.AddAsync(new Bank
        {
            Name = "Integration Test",
            Description = "Some description",
            OwnerId = 1
        })).Entity;
        await appContext.SaveChangesAsync();

        // Act
        await apiTestFixture.PerformDelete($"banks/{newBank.Id}");

        // Assert
        Bank? bank = await appContext.Banks.SingleOrDefaultAsync(e => e.Id == newBank.Id);
        bank.Should().BeNull();
    }

    [Fact]
    public async Task Delete_Should_Rise_WrongBankOwnerException()
    {
        // Arrange
        using ApiTestFixture apiTestFixture = TestUtils.BuildAuthorizedAPITestFixture();

        const int BANK_ID_TO_DELETE = 3;

        // Act && Assert
        var exception = await Assert.ThrowsAsync<MessageConsumerException>(async () =>
            await apiTestFixture.PerformDelete($"banks/{BANK_ID_TO_DELETE}"));

        exception.InnerException!.Should().BeOfType(typeof(WrongBankOwnerException));
    }

    [Fact]
    public async Task Delete_Multiply_Should_Be_OK()
    {
        // Arrange
        using ApiTestFixture apiTestFixture = TestUtils.BuildAuthorizedAPITestFixture();

        // Act
        await apiTestFixture.PerformDelete($"banks?ids=1&&ids=2");

        // Asserts
        using IServiceScope scope = apiTestFixture.Services.CreateScope();
        await using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        (await appContext.Banks.FindAsync(1)).Should().BeNull();
        (await appContext.Banks.FindAsync(2)).Should().BeNull();
    }
}