namespace Services.Transactions.Create;

using Domain;
using Domain.Finances.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using Services.Transactions.Create.Transfer;
using SlimMessageBus;

public record TransactionsCreateRequest : IRequest<int>, ITransactional
{
    public int ActorId { get; set; }
    public decimal Amount { get; init; }

    /// <summary>
    ///     <see cref="Transaction.Category" />
    /// </summary>
    public int? CategoryId { get; set; }

    public bool Confirmed { get; set; }

    public string? Description { get; init; }
    public int? DestinationAccountId { get; set; }
    public decimal? ExchangeRate { get; set; }
    public int? FamilyMemberId { get; set; }
    public GeoLocationPosition? GeoLocationPosition { get; set; }
    public int SourceAccountId { get; init; }
    public DateTime? TransactionDateTime { get; set; }
    public IDbContextTransaction? DbContextTransaction { get; set; }
}