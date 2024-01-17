namespace Services.Transactions.CheckAvailabilityForChanges;

using DAL.Abstract.Transaction;
using Serilog;
using Services.Core;

public class TransactionsContainsLockedRequestHandler(
    ILogger logger,
    ITransactionsRepository transactionsRepository) : RequestHandler<
    TransactionsContainsLockedRequest, bool>(logger)
{
    protected override async Task<bool> DoOnHandle(TransactionsContainsLockedRequest request)
    {
        return await transactionsRepository.ContainsLocked(request.Ids);
    }
}