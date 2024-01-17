using System.Net;
using DAL.EF;
using Domain.Finances.Transactions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Services.TransactionCategories.GetByOwner;
using Web.API.IntegrationTests.Core;
using Web.API.Models;
using Web.API.Models.TransactionCategories;
using Web.API.Models.Users.Authenticate;
using Xunit;

namespace Web.API.IntegrationTests;

public class TransactionCategoriesControllerTests
{
    [Fact]
    public async Task GetByOwner_ShouldReturn_51()
    {
        using ApiTestFixture apiTestFixture = TestUtils.BuildAuthorizedAPITestFixture();

        var response =
            await apiTestFixture.PerformGet<GetByOwnerTransactionCategoriesResponse>("transaction-categories/my");

        response!.Items.Length.Should().Be(51);
    }

    [Fact]
    public async Task Add_ShouldReturn_400_WhenItIsAlreadyExists()
    {
        using ApiTestFixture apiTestFixture = TestUtils.BuildAuthorizedAPITestFixture();

        var addTransactionCategoryModel = new AddOrUpdateTransactionCategoryModel
        {
            Name = "Відпочинок",
            Description = "Test description"
        };
        try
        {
            await apiTestFixture.PerformPost("transaction-categories", addTransactionCategoryModel);
        }
        catch (ApiTestFixtureResponseException ex)
        {
            ex.ResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            (await ex.ResponseMessage.Content
                    .ReadAsStringAsync())
                .Should()
                .Contain("Transaction category with Name Відпочинок already exists.");
        }
    }

    [Fact]
    public async Task Add_ShouldReturn_401()
    {
        using var apiTestFixture = new ApiTestFixture();

        var addTransactionCategoryModel = new AddOrUpdateTransactionCategoryModel
        {
            Name = "Відпочинок",
            Description = "Test description"
        };
        try
        {
            await apiTestFixture.PerformPost("transaction-categories", addTransactionCategoryModel);
        }
        catch (ApiTestFixtureResponseException ex)
        {
            ex.ResponseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }

    [Fact]
    public async Task Add_ShouldAddNewCategory()
    {
        using ApiTestFixture apiTestFixture = TestUtils.BuildAuthorizedAPITestFixture();

        var addTransactionCategoryModel = new AddOrUpdateTransactionCategoryModel
        {
            Name = "Some root category",
            Description = "Test description"
        };

        int actual = await apiTestFixture.PerformPost<int>("transaction-categories", addTransactionCategoryModel);

        actual.Should().Be(104);
    }

    [Fact]
    public async Task Delete_ShouldReturn_BadRequest_DeleteSubCategoriesFirst()
    {
        using ApiTestFixture apiTestFixture = TestUtils.BuildAuthorizedAPITestFixture();

        try
        {
            await apiTestFixture.PerformDelete("transaction-categories/43");
        }
        catch (ApiTestFixtureResponseException ex)
        {
            ex.ResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            string responseContent = await ex.ResponseMessage.Content
                .ReadAsStringAsync();

            var modelState = JsonConvert.DeserializeObject<RequestError>(responseContent)!;

            modelState.Errors.Count.Should().Be(1);
            modelState.Errors[0].ErrorMessage.Should()
                .Be("Category with id 43 contains subcategories. Please, delete them first");
        }
    }

    [Fact]
    public async Task Delete_ShouldReturn_BadRequest_NoSuchCategory()
    {
        using var apiTestFixture = new ApiTestFixture(new AuthenticateUserInput
        {
            Password = "PH44szAV",
            Username = "family.budget"
        });

        try
        {
            await apiTestFixture.PerformDelete("transaction-categories/43");
        }
        catch (ApiTestFixtureResponseException ex)
        {
            ex.ResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            string responseContent = await ex.ResponseMessage.Content
                .ReadAsStringAsync();

            var modelState = JsonConvert.DeserializeObject<RequestError>(responseContent)!;
            modelState.Errors.Count.Should().Be(1);
            modelState.Errors[0].ErrorMessage.Should().Be("Category with id 43 does not exist");
        }
    }

    [Fact]
    public async Task Delete()
    {
        using ApiTestFixture apiTestFixture = TestUtils.BuildAuthorizedAPITestFixture();

        await apiTestFixture.PerformDelete("transaction-categories/3");

        Assert.True(true);
    }

    [Fact]
    public async Task Update()
    {
        using ApiTestFixture apiTestFixture = TestUtils.BuildAuthorizedAPITestFixture();

        await apiTestFixture.PerformUpdate("transaction-categories/3", new AddOrUpdateTransactionCategoryModel
        {
            Name = "Кефір",
            Description = "Some description",
            ParentId = 2
        });

        using IServiceScope scope = apiTestFixture.Services.CreateScope();
        await using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        TransactionCategory category = (await appContext.TransactionCategories.FindAsync(3))!;

        category.Name.Should().Be("Кефір");
        category.Description.Should().Be("Some description");
        category.ParentId.Should().Be(2);
    }
}