namespace Services.Transactions.Create.Transfer;

using Domain.Finances.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using SlimMessageBus;

public interface ITransactional
{
    IDbContextTransaction? DbContextTransaction { get; set; }
}

public class TransferTransactionCreateRequest : IRequest<int>, ITransactional
{
    public int ActorId { get; init; }
    public decimal Amount { get; init; }

    /// <summary>
    ///     <see cref="Transaction.Category" />
    /// </summary>
    public int? CategoryId { get; set; }

    public bool Confirmed { get; set; }

    public string? Description { get; init; }
    public int DestinationAccountId { get; set; }

    public decimal? ExchangeRate { get; set; }
    public int? FamilyMemberId { get; set; }
    public int SourceAccountId { get; set; }
    public DateTime? TransactionDateTime { get; set; }
    public IDbContextTransaction? DbContextTransaction { get; set; }
}