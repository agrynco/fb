using DAL.Abstract.TransactionCategories;
using Serilog;
using Services.Core;

namespace Services.TransactionCategories.IsExistsWithId;

public class
    IsTransactionCategoryWithIdExistsRequestHandler(ILogger logger,
        ITransactionCategoriesRepository transactionCategoriesRepository)
    : RequestHandler<IsTransactionCategoryWithIdExistsRequest, bool>(logger)
{
    protected override async Task<bool> DoOnHandle(IsTransactionCategoryWithIdExistsRequest request)
    {
        return await transactionCategoriesRepository.IsExists(request.OwnerId, request.Id);
    }
}