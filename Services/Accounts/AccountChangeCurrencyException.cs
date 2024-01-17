namespace Services.Accounts;

using Services.Exceptions;

[Serializable]
public class AccountChangeCurrencyException : ServiceException
{
    public AccountChangeCurrencyException(int accountId, int desiredCurrencyId)
        : base("There are transactions with this account")
    {
        AccountId = accountId;
        DesiredCurrencyId = desiredCurrencyId;
    }

    public int AccountId { get; }
    public int DesiredCurrencyId { get; }
}