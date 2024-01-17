namespace Services.Banks;

using Domain;
using Domain.Finances;
using Services.Exceptions;

public class WrongBankOwnerException(
        int[] bankIds, int ownerId)
    : ServiceException(
        $"{nameof(User)} with {nameof(User.Id)} = {ownerId} does not own some of {nameof(Bank)}s with " +
        $"{nameof(Bank.Id)} in [{string.Join(", ", bankIds)}]")
{
    public int[] BankIds { get; } = bankIds;
    public int OwnerId { get; } = ownerId;
}