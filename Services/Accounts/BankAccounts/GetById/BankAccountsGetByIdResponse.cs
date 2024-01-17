namespace Services.Accounts.BankAccounts.GetById;

using DAL.Abstract.Accounts.CashAccounts;

public record BankAccountsGetByIdResponse : AccountsGetByIdResponseBase
{
    public string IBAN { get; set; } = default!;
    public BankDto Bank { get; set; } = default!;
}