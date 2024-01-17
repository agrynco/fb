namespace Services.Transactions.Update;

using Domain;
using Domain.Finances.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using Services.Transactions.Create.Transfer;

public class TransactionUpdateMessage : ITransactional
{
    public int ActorId { get; set; }
    public decimal Amount { get; init; }

    /// <summary>
    ///     <see cref="Transaction.Category" />
    /// </summary>
    public int? CategoryId { get; init; }

    public bool Confirmed { get; init; }

    public string? Description { get; init; }
    public int? DestinationAccountId { get; init; }
    public decimal? ExchangeRate { get; init; }
    public int? FamilyMemberId { get; init; }
    public GeoLocationPosition? GeoLocationPosition { get; set; }
    public int Id { get; set; }
    public int SourceAccountId { get; init; }
    public DateTime? TransactionDateTime { get; init; }
    public IDbContextTransaction? DbContextTransaction { get; set; }
}