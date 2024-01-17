using DAL.Abstract.TransactionCategories;
using Serilog;
using Services.Core;

namespace Services.TransactionCategories.Delete;

public class DeleteTransactionCategoryMessageConsumer : MessageConsumer<DeleteTransactionCategoryMessage>
{
    private readonly ITransactionCategoriesRepository _transactionCategoriesRepository;

    public DeleteTransactionCategoryMessageConsumer(ILogger logger,
        ITransactionCategoriesRepository transactionCategoriesRepository) : base(logger)
    {
        _transactionCategoriesRepository = transactionCategoriesRepository;
    }

    protected override async Task DoOnHandle(DeleteTransactionCategoryMessage message)
    {
        await _transactionCategoriesRepository.Delete(message.Id);
    }
}