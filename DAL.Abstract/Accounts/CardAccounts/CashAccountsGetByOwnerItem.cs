namespace DAL.Abstract.Accounts.CashAccounts;

public record CardAccountsGetByOwnerItem : AccountsGetByOwnerItemBase
{
    public BankDto Bank { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public bool Verified { get; set; }
}