namespace Domain.Finances.Transactions;

using System.Globalization;

public class Transaction : Entity
{
    private DateTime _transactionDateTime;
    public Account Account { get; set; } = null!;
    public int AccountId { get; set; }

    /// <summary>
    ///     <see cref="User" /> who initiated the transaction. Actually, it's the user who owns the <see cref="Account" />.
    ///     So if the current <see cref="User" /> is the owner of the <see cref="Account" />, then this
    ///     <see cref="Transaction" /> is visible for the <see cref="User" />,
    ///     otherwise not.
    /// </summary>
    public User Actor { get; set; } = null!;

    public int ActorId { get; set; }

    public decimal Amount { get; set; }

    public TransactionCategory? Category { get; set; }
    public int? CategoryId { get; set; }
    public bool Confirmed { get; set; }
    public int DayOfWeek { get; private set; }
    public string? Description { get; set; }

    public FamilyMember? FamilyMember { get; set; } = null!;
    public int? FamilyMemberId { get; set; }

    public GeoLocationPosition GeoLocationPosition { get; set; }
    public int? GeoLocationPositionId { get; set; }
    public int MonthOfYear { get; private set; }

    public TransactionCorrelation? TransactionCorrelation { get; set; }
    public int? TransactionCorrelationId { get; set; }

    public DateTime TransactionDateTime
    {
        get => _transactionDateTime;
        set
        {
            _transactionDateTime = value;
            TransactionWeekOfYear = ISOWeek.GetWeekOfYear(value);
            MonthOfYear = value.Month;
            DayOfWeek = (int)value.DayOfWeek;
        }
    }

    public int TransactionWeekOfYear { get; private set; }
}