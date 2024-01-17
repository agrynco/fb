using SlimMessageBus;

namespace Services.TransactionCategories.Add;

public class AddTransactionCategoryRequest : IRequest<int>
{
    public string? Description { get; init; }

    public required string Name { get; init; } = default!;
    public int OwnerId { get; init; }
    public int? ParentId { get; init; }
}