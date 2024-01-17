namespace DAL.Abstract.Accounts.CashAccounts;

public record CashAccountsGetByOwnerItem : AccountsGetByOwnerItemBase
{
    public bool Verified { get; set; }
}