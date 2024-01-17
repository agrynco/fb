namespace Services.Accounts.CashAccounts.GetByOwner;

using DAL.Abstract.Accounts.CashAccounts;
using Serilog;
using Services.Core;

public class CashAccountsGetByOwnerRequestHandler(ILogger logger,
    ICashAccountsRepository accountsRepository) : RequestHandler<CashAccountsGetByOwnerRequest, object>(logger)
{
    protected override async Task<object> DoOnHandle(CashAccountsGetByOwnerRequest request)
    {
        return await Task.FromResult(accountsRepository.GetByOwner(request.OwnerId, request.LoadOptions));
    }
}