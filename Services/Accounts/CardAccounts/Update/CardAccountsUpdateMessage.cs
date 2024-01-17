namespace Services.Accounts.CardAccounts.Update;

public record CardAccountsUpdateMessage : AccountsUpdateMessageBase
{
    public string CardNumber { get; set; } = default!;
    public int BankId { get; set; }
}