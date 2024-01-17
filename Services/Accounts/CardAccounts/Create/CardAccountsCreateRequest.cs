namespace Services.Accounts.CardAccounts.Create;

using SlimMessageBus;

public record CardAccountsCreateRequest: AccountsUpdateMessageBase, IRequest<int>
{
    public string CardNumber { get; set; } = default!;
    public int BankId { get; set; }
}