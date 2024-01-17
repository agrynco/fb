using DAL.Abstract.TransactionCategories;
using Serilog;
using Services.Core;

namespace Services.TransactionCategories.FindByName;

public class FindByNameRequestHandler : RequestHandler<FindByNameRequest, int?>
{
    private readonly ITransactionCategoriesRepository _transactionCategoriesRepository;

    public FindByNameRequestHandler(ILogger logger,
        ITransactionCategoriesRepository transactionCategoriesRepository) : base(logger)
    {
        _transactionCategoriesRepository = transactionCategoriesRepository;
    }

    protected override async Task<int?> DoOnHandle(FindByNameRequest request)
    {
        return await _transactionCategoriesRepository.FindByName(request.Name, request.OwnerId, request.ParentId);
    }
}