namespace Services.Transactions.Get;

using DAL.Abstract.Transaction;
using SlimMessageBus;

public class TransactionsGetRequest : IRequest<object>
{
    public TransactionsDataSourceLoadOptions LoadOptions { get; init; } = default!;
    public int ActorId { get; init; }
    public required string LanguageKey { get; init; }
}