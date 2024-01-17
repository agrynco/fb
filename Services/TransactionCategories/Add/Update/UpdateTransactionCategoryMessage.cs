namespace Services.TransactionCategories.Add.Update;

public class UpdateTransactionCategoryMessage
{
    public string? Description { get; init; }
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public int OwnerId { get; init; }
    public int? ParentId { get; init; }
}