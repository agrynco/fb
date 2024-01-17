namespace Domain.Finances;

public class BankAccount : Account
{
    public BankAccount()
    {
        Type = AccountType.Bank;
    }

    public Bank Bank { get; set; } = null!;
    public int BankId { get; set; }

    /// <summary>
    ///     International Bank Account Number
    /// </summary>
    public required string IBAN { get; set; } = default!;
}