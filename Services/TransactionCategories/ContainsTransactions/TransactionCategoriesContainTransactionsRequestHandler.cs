namespace Services.TransactionCategories.ContainsTransactions;

using DAL.Abstract.Transaction;
using Serilog;
using Services.Core;

public class
    TransactionCategoriesContainTransactionsRequestHandler(ILogger logger, ITransactionsRepository transactionsRepository) : RequestHandler<
        TransactionCategoriesContainTransactionsRequest, bool>(logger)
{
    protected override async Task<bool> DoOnHandle(TransactionCategoriesContainTransactionsRequest request)
    {
        int[] categoryIds = await transactionsRepository.GetCategoryIds(request.ActorId);
        
        return request.CategoryIds.Intersect(categoryIds).Any();
    }
}