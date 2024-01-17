using Microsoft.EntityFrameworkCore.Storage;
using Services.Transactions.Create.Transfer;

namespace Services.Transactions.Delete.Transfer;

public class TransactionsDeleteTransferMessage : ITransactional
{
    public int ActorId { get; init; }
    public int TransactionCorrelationId { get; init; }
    public IDbContextTransaction? DbContextTransaction { get; set; }
}