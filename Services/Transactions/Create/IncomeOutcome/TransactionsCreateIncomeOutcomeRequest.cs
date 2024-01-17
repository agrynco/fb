namespace Services.Transactions.Create.IncomeOutcome;

using Domain;
using Microsoft.EntityFrameworkCore.Storage;
using Services.Transactions.Create.Transfer;
using SlimMessageBus;

public class TransactionsCreateIncomeOutcomeRequest : IRequest<int>, ITransactional
{
    public int ActorId { get; init; }

    public decimal Amount { get; init; }

    /// <summary>
    ///     <see cref="Domain.Finances.Transactions.Transaction.Category" />
    /// </summary>
    public int? CategoryId { get; init; }

    public bool Confirmed { get; set; }

    public string? Description { get; init; }
    public int? FamilyMemberId { get; set; }
    public GeoLocationPosition? GeoLocationPosition { get; set; } = null!;
    public int TargetAccountId { get; set; }
    public DateTime? TransactionDateTime { get; init; }
    public IDbContextTransaction? DbContextTransaction { get; set; }
}