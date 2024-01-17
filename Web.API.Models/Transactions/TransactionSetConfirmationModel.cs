namespace Web.API.Models.Transactions;

public record TransactionSetConfirmationModel
{
    public bool Confirmed { get; init; }
}