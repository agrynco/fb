using AutoMapper;
using DAL.Abstract.Currencies;
using Serilog;
using Services.Core;

namespace Services.Currencies.GetAll;

public class GetAllCurrenciesRequestHandler : RequestHandler<GetAllCurrenciesRequest,
    GetAllCurrenciesResponse>
{
    private readonly ICurrenciesRepository _currenciesRepository;
    private readonly IMapper _mapper;

    public GetAllCurrenciesRequestHandler(ILogger logger,
        ICurrenciesRepository currenciesRepository, IMapper mapper) : base(logger)
    {
        _currenciesRepository = currenciesRepository;
        _mapper = mapper;
    }

    protected override async Task<GetAllCurrenciesResponse> DoOnHandle(GetAllCurrenciesRequest request)
    {
        var currenciesGetAllItemResults = await _currenciesRepository.GetAll();

        return new GetAllCurrenciesResponse(_mapper.Map<GetAllCurrenciesItem[]>(currenciesGetAllItemResults));
    }
}