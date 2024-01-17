namespace Services.TransactionCategories.ContainsTransactions;

using Domain.Finances.Transactions;
using SlimMessageBus;

public record TransactionCategoriesContainTransactionsRequest : IRequest<bool>
{
    public int[] CategoryIds { get; init; } = Array.Empty<int>();
    
    /// <summary>
    /// <see cref="Transaction.Actor"/>
    /// </summary>
    public int ActorId { get; init; }
}