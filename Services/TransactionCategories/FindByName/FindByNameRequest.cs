using SlimMessageBus;

namespace Services.TransactionCategories.FindByName;

public class FindByNameRequest : IRequest<int?>
{
    public required string Name { get; init; } = default!;
    public int OwnerId { get; init; }
    public int? ParentId { get; init; }
}