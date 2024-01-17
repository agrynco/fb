namespace Domain.Finances;

public class CashAccount : Account
{
    public CashAccount()
    {
        Type = AccountType.Cash;
    }
}