namespace Services.Transactions.Delete;

using Microsoft.EntityFrameworkCore.Storage;
using Services.Transactions.Create.Transfer;

public class TransactionsDeleteMessage : ITransactional
{
    public int ActorId { get; init; }
    public int[] Ids { get; init; } = Array.Empty<int>();
    public IDbContextTransaction? DbContextTransaction { get; set; }
}