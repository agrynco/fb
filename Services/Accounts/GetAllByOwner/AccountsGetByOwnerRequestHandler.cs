namespace Services.Accounts.GetAll;

using DAL.Abstract.Accounts;
using Serilog;
using Services.Core;

public class AccountsGetByOwnerRequestHandler(
        ILogger logger,
        IAccountsRepository accountsRepository)
    : RequestHandler<AccountsGetByOwnerRequest,
        object>(logger)
{
    protected override async Task<object> DoOnHandle(AccountsGetByOwnerRequest request)
    {
        return await Task.FromResult(accountsRepository.GetByOwner(request.OwnerId, request.LoadOptions));
    }
}