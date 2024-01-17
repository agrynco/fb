// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Services.TransactionCategories.GetByOwner;
using Web.API.IntegrationTests;
using Web.API.IntegrationTests.Core;

using ApiTestFixture apiTestFixture = TestUtils.BuildAuthorizedAPITestFixture();

void PerformRequestAction()
{
    apiTestFixture.PerformGet<GetByOwnerTransactionCategoriesResponse>("transaction-categories/my");
    Console.WriteLine("Hello World!");
}

// Setup

void Run(int numberOfTimes, int degreeOfParallelism = 4)
{
    // Setup
    var watch = Stopwatch.StartNew();

    // Define a ParallelOptions instance
    var parallelOptions = new ParallelOptions()
    {
        MaxDegreeOfParallelism = degreeOfParallelism
    };

    // Run the method multiple times in parallel
    Parallel.For(0, numberOfTimes, parallelOptions, (count, _) =>
    {
        PerformRequestAction();
        Console.WriteLine($"Completed {count + 1} operations");
    });

    // Print diagnostic information
    watch.Stop();
    var elapsedMs = watch.ElapsedMilliseconds;
    Console.WriteLine($"Completed {numberOfTimes} operations in {elapsedMs} ms");
}

Run(1000, 100);