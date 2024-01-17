namespace Web.API.IntegrationTests;

using DAL.Abstract.Transaction;
using DAL.EF;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Transactions;
using Web.API.IntegrationTests.Core;
using Web.API.Models.Transactions;
using Xunit;

public class TransactionsControllerTests
{
    [Fact]
    public async Task CreateIncomeTransaction()
    {
        using ApiTestFixture apiTestFixture = TestUtils.BuildAuthorizedAPITestFixture();

        int transactionId = await apiTestFixture.PerformPost<int>("transactions", new TransactionCreateOrUpdateModel
        {
            SourceAccountId = 9,
            Amount = 100,
            Description = "Test income transaction",
            CategoryId = 1,
            FamilyMemberId = 18
        });

        using IServiceScope scope = apiTestFixture.Services.CreateScope();
        await using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var account = await appContext.Accounts.SingleAsync(e => e.Id == 9);
        account.Balance.Should().Be(100);

        var transaction = await appContext.Transactions.SingleAsync(e => e.Id == transactionId);
        transaction.CategoryId.Should().Be(1);
        transaction.FamilyMemberId.Should().Be(18);
    }

    [Fact]
    public async Task CreateIncomeTransaction_ShouldFailBecauseOfClosedAccount()
    {
        // Arrange
        using ApiTestFixture apiTestFixture = TestUtils.BuildAuthorizedAPITestFixture();

        // Act && Assert
        var thereAreClosedAccountsException = await Assert.ThrowsAsync<ThereAreClosedAccountsException>(async () =>
        {
            await apiTestFixture.PerformPost("transactions", new TransactionCreateOrUpdateModel
            {
                SourceAccountId = 13,
                Amount = 100,
                Description = "Test income transaction"
            });
        });

        thereAreClosedAccountsException.Accounts.Should().HaveCount(1);
        thereAreClosedAccountsException.Accounts.First().Id.Should().Be(13);
    }

    [Fact]
    public async Task CreateOutcomeIncomeTransaction()
    {
        using ApiTestFixture apiTestFixture = TestUtils.BuildAuthorizedAPITestFixture();

        await apiTestFixture.PerformPost("transactions", new TransactionCreateOrUpdateModel
        {
            SourceAccountId = 9,
            Amount = -100,
            Description = "Test income transaction"
        });

        using IServiceScope scope = apiTestFixture.Services.CreateScope();
        await using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var account = await appContext.Accounts.SingleAsync(e => e.Id == 9);
        account.Balance.Should().Be(-100);
    }

    [Fact]
    public async Task CreateTransferTransactionWithTheSameCurrency()
    {
        using ApiTestFixture apiTestFixture = TestUtils.BuildAuthorizedAPITestFixture();

        int transactionId = await apiTestFixture.PerformPost<int>("transactions", new TransactionCreateOrUpdateModel
        {
            SourceAccountId = 9,
            DestinationAccountId = 2,
            Amount = 50,
            Description = "Test transfer transaction",
            CategoryId = 1,
            FamilyMemberId = 18,
            Confirmed = true
        });

        using IServiceScope scope = apiTestFixture.Services.CreateScope();
        await using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var sourceAccount = await appContext.Accounts.SingleAsync(e => e.Id == 9);
        sourceAccount.Balance.Should().Be(-50);

        var destinationAccount = await appContext.Accounts.SingleAsync(e => e.Id == 2);
        destinationAccount.Balance.Should().Be(50);

        var transaction = await appContext.Transactions.SingleAsync(e => e.Id == transactionId);
        transaction.CategoryId.Should().Be(1);

        transaction.FamilyMemberId.Should().Be(18);

        var transactionsCorrelation = await appContext.TransactionCorrelations
            .Where(x => x.IncomeTransactionId == transactionId || x.OutcomeTransactionId == transactionId)
            .SingleAsync();
        
        int secondTransactionId = transactionsCorrelation.GetSecondTransactionId(transactionId);
        var secondTransaction = await appContext.Transactions.SingleAsync(e => e.Id == secondTransactionId);
        secondTransaction.FamilyMemberId.Should().Be(18);
        secondTransaction.Confirmed.Should().Be(true);

        transactionId.Should().NotBe(secondTransactionId);
        transaction.Confirmed.Should().Be(true);
    }

    [Fact]
    public async Task CreateTransferTransactionWithTheSameCurrency_ShouldFailBecauseOfClosedAccount()
    {
        using ApiTestFixture apiTestFixture = TestUtils.BuildAuthorizedAPITestFixture();

        var thereAreClosedAccountsException = await Assert.ThrowsAsync<ThereAreClosedAccountsException>(async () =>
        {
            await apiTestFixture.PerformPost("transactions", new TransactionCreateOrUpdateModel
            {
                SourceAccountId = 9,
                DestinationAccountId = 13,
                Amount = 50,
                Description = "Test transfer transaction"
            });
        });

        thereAreClosedAccountsException.Accounts.Should().HaveCount(1);
        thereAreClosedAccountsException.Accounts.First().Id.Should().Be(13);
    }

    [Fact]
    public async Task GetTransactions()
    {
        using ApiTestFixture apiTestFixture = TestUtils.BuildAuthorizedAPITestFixture();
        DevExpressResponse<TransactionItem> transactionsGetResponse =
            (await apiTestFixture.PerformGet<DevExpressResponse<TransactionItem>>("transactions?languageKey=en&&Size=10"))!;
        transactionsGetResponse.Data.Length.Should().Be(7);
        transactionsGetResponse.Data.Length.Should().Be(7);
    }
}