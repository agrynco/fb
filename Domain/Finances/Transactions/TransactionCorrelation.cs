namespace Domain.Finances.Transactions;

public class TransactionCorrelation : Entity
{
    public decimal ExchangeRate { get; set; }
    public int IncomeTransactionId { get; set; }
    public int OutcomeTransactionId { get; set; }
    
    public int GetSecondTransactionId(int firstTransactionId)
    {
        return firstTransactionId == IncomeTransactionId ? OutcomeTransactionId : IncomeTransactionId;
    }
}