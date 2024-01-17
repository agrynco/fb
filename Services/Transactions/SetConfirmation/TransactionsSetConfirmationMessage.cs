namespace Services.Transactions.SetConfirmation;

public class TransactionsSetConfirmationMessage
{
    public required int ActorId { get; init; }
    public required bool Confirmed { get; set; }
    public required int Id { get; init; }
}