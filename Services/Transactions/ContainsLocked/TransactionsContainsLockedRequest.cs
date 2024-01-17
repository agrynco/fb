namespace Services.Transactions.CheckAvailabilityForChanges;

using SlimMessageBus;

public record TransactionsContainsLockedRequest : IRequest<bool>
{    
    public int[] Ids { get; init; }
    public int ActorId { get; init; }
}