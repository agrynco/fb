using DAL.Abstract.TransactionCategories;
using Domain.Finances.Transactions;
using Serilog;
using Services.Core;

namespace Services.TransactionCategories.Add.Update;

public class UpdateTransactionCategoryMessageConsumer(
        ILogger logger,
        ITransactionCategoriesRepository transactionCategoriesRepository)
    : MessageConsumer<UpdateTransactionCategoryMessage>(logger)
{
    protected override async Task DoOnHandle(UpdateTransactionCategoryMessage message)
    {
        TransactionCategory? toUpdate = await transactionCategoriesRepository.GetForEdit(message.Id);
        
        if (toUpdate is null)
        {
            throw new InvalidOperationException($"Transaction category with id {message.Id} not found");
        }

        toUpdate.Name = message.Name;
        toUpdate.ParentId = message.ParentId;
        toUpdate.Description = message.Description;

        await transactionCategoriesRepository.Update(toUpdate);
    }
}