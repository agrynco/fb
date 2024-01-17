namespace Services.Transactions.Delete;

using DAL.Abstract;
using DAL.Abstract.Core;
using Domain.Finances.Transactions;
using Serilog;
using Services.Core;
using Services.Transactions.CheckAvailabilityForChanges;
using Services.Transactions.Delete.IncomeOutcome;
using SlimMessageBus;

public class TransactionsDeleteMessageConsumer(
    ILogger logger,
    IMessageBus messageBus,
    ITransactionCorrelationsRepository transactionCorrelationsRepository,
    IUnitOfWork unitOfWork)
    : MessageConsumer<TransactionsDeleteMessage>(logger)
{
    protected override async Task DoOnHandle(TransactionsDeleteMessage message)
    {
        HashSet<int> transactionIds = [..message.Ids];

        await ValidateOnContainsLockedTransactions(message.Ids);

        await new Func<Task>(async () =>
        {
            while (transactionIds.Count != 0)
            {
                int transactionId = transactionIds.First();

                TransactionCorrelation? transactionCorrelation =
                    await transactionCorrelationsRepository.GetByTransaction(transactionId);

                await DeleteCorrelatedTransaction(message, transactionCorrelation, transactionId, transactionIds);

                await DeleteTransaction(message, transactionId, transactionIds);

                if (transactionCorrelation != null)
                {
                    await transactionCorrelationsRepository.Delete(transactionCorrelation.Id);
                }
            }
        }).ExecuteInTransactionAsync(unitOfWork, message);
    }

    /// <summary>
    /// Deletes a transaction with ID = transactionId
    /// </summary>
    /// <param name="message">The message containing information about the actor, database transaction, and transactionId.</param>
    /// <param name="transactionId">The Id of the transaction to be deleted.</param>
    /// <param name="transactionIds">The list of transactionIds from which to remove the transactionId.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task DeleteTransaction(TransactionsDeleteMessage message, int transactionId,
        HashSet<int> transactionIds)
    {
        await messageBus.Publish(new TransactionsDeleteIncomeOutcomeMessage
        {
            Id = transactionId,
            ActorId = message.ActorId,
            DbContextTransaction = message.DbContextTransaction
        });
        transactionIds.Remove(transactionId);
    }

    private async Task DeleteCorrelatedTransaction(TransactionsDeleteMessage message,
        TransactionCorrelation? transactionCorrelation, int transactionId, HashSet<int> transactionIds)
    {
        if (transactionCorrelation != null)
        {
            int correlatedTransactionId = transactionCorrelation.IncomeTransactionId == transactionId
                ? transactionCorrelation.OutcomeTransactionId
                : transactionCorrelation.IncomeTransactionId;

            await messageBus.Publish(new TransactionsDeleteIncomeOutcomeMessage
            {
                Id = correlatedTransactionId,
                ActorId = message.ActorId,
                DbContextTransaction = message.DbContextTransaction
            });

            transactionIds.Remove(correlatedTransactionId);
        }
    }

    private async Task ValidateOnContainsLockedTransactions(int[] transactionIds)
    {
        bool containsLocked = await messageBus.Send(new TransactionsContainsLockedRequest
        {
            Ids = transactionIds
        });

        if (containsLocked)
        {
            throw new TransactionsContainsLockedTransactionsException(transactionIds);
        }
    }
}