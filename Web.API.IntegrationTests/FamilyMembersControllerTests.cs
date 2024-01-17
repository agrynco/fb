namespace Web.API.IntegrationTests;

using DAL.Abstract.FamilyMembers;
using DAL.EF;
using Domain;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Web.API.IntegrationTests.Core;
using Web.API.Models.Users.Authenticate;
using Xunit;

public class FamilyMembersControllerTests
{
    private ApiTestFixture BuildTestFixture()
    {
        return new ApiTestFixture(new AuthenticateUserInput
        {
            Username = "test",
            Password = "Qtcq5VwrDf"
        });
    }
    
    [Fact]
    public async Task GetMyFamilyMembers_ShouldBeOk()
    {
        // Arrange
        using var apiTestFixture = BuildTestFixture();

        // Act
        var actual =
            (await apiTestFixture
                .PerformGet<DevExpressResponse<FamilyBudgetsGetAllResultItem>>("family-members/my"))!;

        // Assert
        actual.Data.Length.Should().Be(17);
    }
    
    [Fact]
    public async Task Delete_ShouldBeOk()
    {
        // Arrange
        using var apiTestFixture = BuildTestFixture();

        // Act
            await apiTestFixture.PerformDelete("family-members?ids=1");

        // Assert
        using IServiceScope scope = apiTestFixture.Services.CreateScope();
        await using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        appContext.FamilyMembers.SingleOrDefault(e => e.Id == 1).Should().BeNull();
    }
    
    [Fact]
    public async Task Get_ShouldBeOk()
    {
        // Arrange
        using var apiTestFixture = BuildTestFixture();

        // Act
        var actual =
            (await apiTestFixture
                .PerformGet<FamilyMemberDto>("family-members/1"))!;

        // Assert
        actual.Id.Should().Be(1);
        actual.Name.Should().Be("Мама");
        actual.OwnerId.Should().Be(3);
    }
    
    [Fact]
    public async Task Update_ShouldBeOk()
    {
        // Arrange
        using var apiTestFixture = BuildTestFixture();

        // Act
        await apiTestFixture.PerformUpdate("family-members/1", new FamilyMemberDto
        {
            Id = 1,
            Name = "QWERTY"
        });

        // Assert
        using IServiceScope scope = apiTestFixture.Services.CreateScope();
        await using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        FamilyMember? actual = appContext.FamilyMembers.SingleOrDefault(e => e.Id == 1);
        actual.Should().NotBeNull();
        actual.Name.Should().Be("QWERTY");
    }
    
    [Fact]
    public async Task Post_ShouldBeOk()
    {
        // Arrange
        using var apiTestFixture = BuildTestFixture();

        // Act
        await apiTestFixture.PerformPost("family-members", new FamilyMemberDto
        {
            Name = "QWERTY",
            OwnerId = 3
        });

        // Assert
        using IServiceScope scope = apiTestFixture.Services.CreateScope();
        await using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        FamilyMember? actual = appContext.FamilyMembers.SingleOrDefault(e => e.Name == "QWERTY");
        actual.Should().NotBeNull();
        actual.OwnerId.Should().Be(3);
    }
}