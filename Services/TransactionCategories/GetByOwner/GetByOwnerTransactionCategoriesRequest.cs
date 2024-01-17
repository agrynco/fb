using SlimMessageBus;

namespace Services.TransactionCategories.GetByOwner;

public class GetByOwnerTransactionCategoriesRequest : IRequest<GetByOwnerTransactionCategoriesResponse>
{
    public int OwnerId { get; init; }
    public int? ParentId { get; init; }
}