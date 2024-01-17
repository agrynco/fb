namespace Domain.Finances;

public class CardAccount : Account
{
    public CardAccount()
    {
        Type = AccountType.Card;
    }

    public Bank Bank { get; set; } = null!;
    public int BankId { get; set; }
    public required string CardNumber { get; set; } = default!;
}