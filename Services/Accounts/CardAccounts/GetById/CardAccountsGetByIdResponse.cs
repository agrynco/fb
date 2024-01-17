namespace Services.Accounts.CardAccounts.GetById;

using DAL.Abstract.Accounts.CashAccounts;

public record CardAccountsGetByIdResponse :  AccountsGetByIdResponseBase
{
    public string CardNumber { get; set; } = default!;
    public BankDto Bank { get;set; } = default!;
}