namespace Services.TransactionCategories.GetById;

using AutoMapper;
using DAL.Abstract.TransactionCategories;
using Domain.Finances.Transactions;
using Serilog;
using Services.Core;

public class TransactionCategoriesGetByIdRequestHandler(
        ILogger logger,
        ITransactionCategoriesRepository transactionCategoriesRepository,
        IMapper mapper)
    : RequestHandler<TransactionCategoriesGetByIdRequest, TransactionCategoriesGetByIdResponse>(logger)
{
    protected override async Task<TransactionCategoriesGetByIdResponse> DoOnHandle(
        TransactionCategoriesGetByIdRequest request)
    {
        TransactionCategory transactionCategory =
            (await transactionCategoriesRepository.GetById(request.Id))!;

        if (transactionCategory.OwnerId != request.OwnerId)
        {
            throw new WrongTransactionCategoryOwnerException(request.Id, request.OwnerId);
        }

        return mapper.Map<TransactionCategoriesGetByIdResponse>(transactionCategory);
    }
}