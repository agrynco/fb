namespace Services.Transactions.GetAccountId;

using DAL.Abstract.Transaction;
using Serilog;
using Services.Core;

public class GetAccountIdsByTransactionIdsRequestHandler(ILogger logger, ITransactionsRepository transactionsRepository)
    : RequestHandler<GetAccountIdsByTransactionIdsRequest, int[]>(logger)
{
    protected override async Task<int[]> DoOnHandle(GetAccountIdsByTransactionIdsRequest request)
    {
        return await transactionsRepository.GetAccountIdsByTransactionIds(request.TransactionIds);
    }
}