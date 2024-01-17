namespace Services.Transactions.Update;

using AutoMapper;
using DAL.Abstract.Core;
using Serilog;
using Services.Core;
using Services.Transactions.Create;
using Services.Transactions.Delete;
using SlimMessageBus;

public class TransactionUpdateMessageConsumer(
    ILogger logger,
    IMessageBus messageBus,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : MessageConsumer<TransactionUpdateMessage>(logger)
{
    protected override async Task DoOnHandle(TransactionUpdateMessage message)
    {
        await new Func<Task>(async () =>
        {
            await messageBus.Publish(new TransactionsDeleteMessage
            {
                Ids = [message.Id],
                ActorId = message.ActorId,
                DbContextTransaction = message.DbContextTransaction
            });

            var transactionsCreateRequest = mapper.Map<TransactionsCreateRequest>(message);
            await messageBus.Publish(transactionsCreateRequest);
        }).ExecuteInTransactionAsync(unitOfWork, message);
    }
}