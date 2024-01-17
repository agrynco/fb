using Services.Exceptions;

namespace Services.Accounts.Delete;

public class AccountsDeleteException(int[] accountIds)
    : ServiceException("Can not delete accounts because some of them contains transactions.")
{
    public int[] AccountIds { get; } = accountIds;
}