namespace DAL.Abstract.Accounts;

using DAL.Abstract.Accounts.CashAccounts;
using Domain.Finances;

public record BankAccountIfo
{
    public string IBAN { get; set; } = default!;
}

public record CardAccountInfo
{
    public string CardNumber { get; set; } = default!;
}

public record WideAccountDto : AccountDto
{
    public BankDto Bank { get; set; }
    public BankAccountIfo BankAccountInfo { get; set; } = default!;
    public CardAccountInfo CardAccountInfo { get; set; } = default!;
    public string Description { get; set; }
    public int OwnerId { get; set; }
    public AccountType Type { get; set; }
    public bool Verified { get; set; }
}