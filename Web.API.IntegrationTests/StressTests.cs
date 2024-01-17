namespace Web.API.IntegrationTests;

using Services.TransactionCategories.GetByOwner;
using Web.API.IntegrationTests.Core;
using Xunit;

public class StressTests
{
    [Fact]
    public async Task ShouldBeOk()
    {
        using ApiTestFixture apiTestFixture = TestUtils.BuildAuthorizedAPITestFixture();

        async Task Action()
        {
            await apiTestFixture.PerformGet<GetByOwnerTransactionCategoriesResponse>("transaction-categories/my");
        }

        int degreeOfParallelism = Environment.ProcessorCount;
        var tasks = new List<Task>();
        var throttler = new SemaphoreSlim(degreeOfParallelism);
        
        for (var i = 0; i < 1000; i++)
        {
            // Wait for previous task or a free slot
            await throttler.WaitAsync();
            tasks.Add(Task.Run(async () =>
            {
                try
                {
                    await Action();
                }
                finally
                {
                    throttler.Release();
                }
            }));
        }

        await Task.WhenAll(tasks);
        
        Assert.True(true);
    }
}