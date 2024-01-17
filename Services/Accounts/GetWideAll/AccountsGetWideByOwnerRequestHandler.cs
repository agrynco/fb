namespace Services.Accounts.GetWideAll;

using DAL.Abstract.Accounts;
using Serilog;
using Services.Core;

public class AccountsGetWideByOwnerRequestHandler(
    ILogger logger,
    IAccountsRepository accountsRepository)
    : RequestHandler<AccountsGetWideByOwnerRequest, object>(logger)
{
    protected override async Task<object> DoOnHandle(AccountsGetWideByOwnerRequest request)
    {
        return await accountsRepository.GetWideByOwnerId(request.AuthorizedUserId, request.LoadOptions);
    }
}