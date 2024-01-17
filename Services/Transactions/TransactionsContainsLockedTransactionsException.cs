namespace Services.Transactions;

using Services.Exceptions;

public class TransactionsContainsLockedTransactionsException(int[] ids)
    : ServiceException($"Transactions with ids {string.Join(", ", ids)} contains one or more locked transactions.")
{
    public int[] Ids { get; init; } = ids;
}