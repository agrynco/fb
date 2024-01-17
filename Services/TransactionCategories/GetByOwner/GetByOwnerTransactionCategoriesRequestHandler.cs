using AutoMapper;
using DAL.Abstract.TransactionCategories;
using Serilog;
using Services.Core;

namespace Services.TransactionCategories.GetByOwner;

public class GetByOwnerTransactionCategoriesRequestHandler : RequestHandler<GetByOwnerTransactionCategoriesRequest,
    GetByOwnerTransactionCategoriesResponse>
{
    private readonly IMapper _mapper;
    private readonly ITransactionCategoriesRepository _transactionCategoriesRepository;

    public GetByOwnerTransactionCategoriesRequestHandler(ILogger logger,
        ITransactionCategoriesRepository transactionCategoriesRepository, IMapper mapper) : base(logger)
    {
        _transactionCategoriesRepository = transactionCategoriesRepository;
        _mapper = mapper;
    }

    protected override async Task<GetByOwnerTransactionCategoriesResponse> DoOnHandle(
        GetByOwnerTransactionCategoriesRequest request)
    {
        var getByOwnerResult = await _transactionCategoriesRepository.Get(request.OwnerId, request.ParentId);

        return new GetByOwnerTransactionCategoriesResponse(
            _mapper.Map<GetByOwnerTransactionCategoryItem[]>(getByOwnerResult));
    }
}