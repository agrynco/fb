namespace Domain.Finances.Transactions;

/// <summary>
///     https://anatolii-hrynchuk.atlassian.net/wiki/spaces/FB/pages/34897921/TransactionCategory
/// </summary>
public class TransactionCategory : NamedEntity
{
    public string? Description { get; set; }
    public User Owner { get; set; } = default!;
    public int OwnerId { get; set; }
    public TransactionCategory? Parent { get; set; }
    public int? ParentId { get; set; }
}