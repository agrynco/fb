using SlimMessageBus;

namespace Services.TransactionCategories.IsExistsWithId;

public class IsTransactionCategoryWithIdExistsRequest : IRequest<bool>
{
    public int Id { get; init; }
    public int OwnerId { get; init; }
}