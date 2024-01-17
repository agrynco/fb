using DAL.Abstract.Transaction;
using Serilog;
using Services.Core;

namespace Services.Transactions.Get;

using DAL.Abstract;

public class TransactionsGetRequestHandler(
    ILogger logger, 
    ITransactionsRepository transactionsRepository,
    ILanguagesRepository languagesRepository)
    : RequestHandler<TransactionsGetRequest, object>(logger)
{
    protected override async Task<object> DoOnHandle(TransactionsGetRequest request)
    {
        return await transactionsRepository.Get(
            request.LoadOptions, 
            request.ActorId,
            (await languagesRepository.GetByKey(request.LanguageKey))!.Id);
    }
}