namespace Services.Accounts.CardAccounts.Update;

public record BankAccountsUpdateMessage : AccountsUpdateMessageBase
{
    public string IBAN { get; set; } = default!;
    public int BankId { get; set; }
}