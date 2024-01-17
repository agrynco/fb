namespace Services.Transactions.SetConfirmation;

using DAL.Abstract.Transaction;
using Serilog;
using Services.Core;

public class TransactionsSetConfirmationMessageConsumer(
    ILogger logger,
    ITransactionsRepository transactionsRepository) 
    : MessageConsumer<TransactionsSetConfirmationMessage>(logger)
{
    protected override async Task DoOnHandle(TransactionsSetConfirmationMessage message)
    {
        var transaction = await transactionsRepository.GetById(message.Id);
        
        if (transaction.ActorId != message.ActorId)
        {
            throw new WrongTransactionOwnerException(transaction.Id, message.ActorId);
        }
        
        transaction.Confirmed = message.Confirmed;
        
        await transactionsRepository.Update(transaction);
    }
}