namespace DAL.Abstract.Transaction;

using DAL.Abstract.Accounts;
using DAL.Abstract.FamilyMembers;
using Domain.Finances.Transactions;

public record TransactionItem
{
    public AccountDto Account { get; set; } = default!;
    public decimal Amount { get; set; }
    public string CategoryName { get; set; } = default!;
    public bool Confirmed { get; set; }
    public int DayOfMonth { get; set; }
    public string DayOfWeek { get; set; }
    public AccountDto? DebitAccount { get; set; }
    public string? Description { get; set; }
    public FamilyMemberDto FamilyMember { get; set; }
    public int Id { get; set; }
    public bool Locked { get; set; }
    public string Month { get; set; }
    public DateTime TransactionDateTime { get; set; }
    public int TransactionWeekOfYear { get; set; }
    public TransactionType Type { get; set; }
    public int Year { get; set; }
}