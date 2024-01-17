namespace Web.API.Models.Transactions;

using Domain.Finances.Transactions;

public record TransactionCreateOrUpdateModel
{
    public decimal Amount { get; init; }

    /// <summary>
    ///     <see cref="Transaction.Category" />
    /// </summary>
    public int? CategoryId { get; init; }

    public bool Confirmed { get; init; }

    public string? Description { get; init; }
    public int? DestinationAccountId { get; init; }

    public decimal? ExchangeRate { get; set; }
    public int? FamilyMemberId { get; init; }

    public GeoLocationPositionModel? GeoLocationPosition { get; set; }
    public int SourceAccountId { get; init; }
    public DateTime? TransactionDateTime { get; set; }
}