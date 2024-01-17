using Domain.Finances.Transactions;

namespace Services.TransactionCategories.GetByOwner;

/// <summary>
/// <seealso cref="TransactionCategory"/>
/// </summary>
public class GetByOwnerTransactionCategoryItem
{
    public int Id { get; set; }
    public required string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int? ParentId { get; set; }
}