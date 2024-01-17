namespace Services.TransactionCategories.GetById;

using Domain;
using Domain.Finances.Transactions;

public class WrongTransactionCategoryOwnerException(
        int transactionCategoryId,
        int ownerId)
    : Exception(
        $"{nameof(User)} with {nameof(User.Id)} = {ownerId} does not own the {nameof(TransactionCategory)} with " +
        $"{nameof(TransactionCategory.Id)} = {transactionCategoryId}")
{
    public int TransactionId { get; } = transactionCategoryId;
    public int OwnerId { get; } = ownerId;
}