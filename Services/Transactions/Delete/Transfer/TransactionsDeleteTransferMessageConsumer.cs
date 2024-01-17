using DAL.Abstract;
using DAL.Abstract.Core;
using Domain.Finances.Transactions;
using Serilog;
using Services.Core;
using Services.Transactions.Delete.IncomeOutcome;
using SlimMessageBus;

namespace Services.Transactions.Delete.Transfer;

public class TransactionsDeleteTransferMessageConsumer : MessageConsumer<TransactionsDeleteTransferMessage>
{
    private readonly IMessageBus _messageBus;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionCorrelationsRepository _transactionCorrelationsRepository;

    public TransactionsDeleteTransferMessageConsumer(ILogger logger, IMessageBus messageBus,
        IUnitOfWork unitOfWork,
        ITransactionCorrelationsRepository transactionCorrelationsRepository) : base(logger)
    {
        _messageBus = messageBus;
        _unitOfWork = unitOfWork;
        _transactionCorrelationsRepository = transactionCorrelationsRepository;
    }

    protected override async Task DoOnHandle(TransactionsDeleteTransferMessage message)
    {
        await new Func<Task>(async () =>
        {
            TransactionCorrelation transactionCorrelation =
                (await _transactionCorrelationsRepository.GetById(message.TransactionCorrelationId))!;

            await _transactionCorrelationsRepository.Delete(message.TransactionCorrelationId);

            await _messageBus.Publish(new TransactionsDeleteIncomeOutcomeMessage
            {
                ActorId = message.ActorId,
                DbContextTransaction = message.DbContextTransaction,
                Id = transactionCorrelation.IncomeTransactionId
            });
            
            await _messageBus.Publish(new TransactionsDeleteIncomeOutcomeMessage
            {
                ActorId = message.ActorId,
                DbContextTransaction = message.DbContextTransaction,
                Id = transactionCorrelation.OutcomeTransactionId
            });
        }).ExecuteInTransactionAsync(_unitOfWork, message);
    }
}