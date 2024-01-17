namespace Services.Transactions.GetAccountId;

using SlimMessageBus;

public class GetAccountIdsByTransactionIdsRequest : IRequest<int[]>
{
    public int[] TransactionIds { get; init; } = Array.Empty<int>();
}