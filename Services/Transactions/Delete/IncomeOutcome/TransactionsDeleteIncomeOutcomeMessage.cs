using Microsoft.EntityFrameworkCore.Storage;
using Services.Transactions.Create.Transfer;

namespace Services.Transactions.Delete.IncomeOutcome;

public class TransactionsDeleteIncomeOutcomeMessage : ITransactional
{
    public int ActorId { get; init; }
    public int Id { get; init; }
    public IDbContextTransaction? DbContextTransaction { get; set; }
}