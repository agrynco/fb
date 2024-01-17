using DAL.Abstract.TransactionCategories;
using Domain.Finances.Transactions;
using Serilog;
using Services.Core;

namespace Services.TransactionCategories.Add;

public class AddTransactionCategoryRequestHandler : RequestHandler<AddTransactionCategoryRequest, int>
{
    private readonly ITransactionCategoriesRepository _transactionCategoriesRepository;

    public AddTransactionCategoryRequestHandler(ILogger logger,
        ITransactionCategoriesRepository transactionCategoriesRepository) : base(logger)
    {
        _transactionCategoriesRepository = transactionCategoriesRepository;
    }

    protected override async Task<int> DoOnHandle(AddTransactionCategoryRequest request)
    {
        var newTransactionCategory = new TransactionCategory
        {
            Name = request.Name,
            Description = request.Description,
            OwnerId = request.OwnerId,
            ParentId = request.ParentId
        };

        return await _transactionCategoriesRepository.Add(newTransactionCategory);
    }
}