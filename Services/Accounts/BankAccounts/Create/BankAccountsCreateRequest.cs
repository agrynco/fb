namespace Services.Accounts.BankAccounts.Create;

using SlimMessageBus;

public record BankAccountsCreateRequest: AccountsUpdateMessageBase, IRequest<int>
{
    public string IBAN { get; set; } = default!;
    public int BankId { get; set; }
}