namespace Services.Transactions.Update;

public class TransactionUpdateException : Exception
{
    public TransactionUpdateException(TransactionUpdateMessage transactionUpdateMessage, Exception innerException)
        : base($"Transaction update failed. {transactionUpdateMessage}", innerException)
    {
        TransactionUpdateMessage = transactionUpdateMessage;
    }
    
    public TransactionUpdateMessage TransactionUpdateMessage { get; }
}