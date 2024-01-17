namespace Services.Transactions;

using DAL.Abstract.Accounts;
using Services.Exceptions;

public class ThereAreClosedAccountsException(AccountDto[] accounts) : ServiceException("There are closed accounts")
{
    public AccountDto[] Accounts { get; } = accounts;
}