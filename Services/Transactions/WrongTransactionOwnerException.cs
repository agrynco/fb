using Domain;
using Domain.Finances.Transactions;

namespace Services.Transactions;

[Serializable]
public class WrongTransactionOwnerException(
        int transactionId,
        int ownerId)
    : Exception($"{nameof(User)} with {nameof(User.Id)} = {ownerId} does not own the {nameof(Transaction)} with " +
                $"{nameof(Transaction.Id)} = {transactionId}")
{
    public int TransactionId { get; } = transactionId;
    public int OwnerId { get; } = ownerId;
}