namespace Services.Transactions.GetForCreateOrEdit;

public class TransactionsGetForEditResponse
{
    public decimal Amount { get; init; }
    public int? CategoryId { get; init; }
    public bool Confirmed { get; init; }
    public string? Description { get; init; } = string.Empty;
    public int? DestinationAccountId { get; init; }
    public decimal? ExchangeRate { get; init; }
    public int? FamilyMemberId { get; init; }
    public int SourceAccountId { get; init; }
    public DateTime TransactionDateTime { get; init; }
}