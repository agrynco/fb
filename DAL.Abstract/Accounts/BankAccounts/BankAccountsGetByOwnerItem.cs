namespace DAL.Abstract.Accounts.BankAccounts;

using DAL.Abstract.Accounts.CashAccounts;

public record BankAccountsGetByOwnerItem : AccountsGetByOwnerItemBase
{
    public BankDto Bank { get; set; } = default!;
    public string IBan { get; set; } = default!;
    public bool Verified { get; set; }
}