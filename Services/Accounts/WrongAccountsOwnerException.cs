using Domain;
using Domain.Finances;
using Services.Exceptions;

namespace Services.Accounts;

[Serializable]
public class WrongAccountsOwnerException(int[] accountIds, int ownerId)
    : ServiceException(
        $"{nameof(User)} with {nameof(User.Id)} = {ownerId} does not own some of {nameof(Account)}(s) with id " +
        $"({string.Join(", ", accountIds.Select(id => id.ToString()))})")
{
    public int[] AccountIds { get; } = accountIds;
    public int OwnerId { get; } = ownerId;
}