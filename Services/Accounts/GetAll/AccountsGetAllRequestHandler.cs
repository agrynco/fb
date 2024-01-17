namespace Services.Accounts.GetAll;

using DAL.Abstract.Accounts;
using Serilog;
using Services.Core;

public class AccountsGetAllRequestHandler(
    ILogger logger,
    IAccountsRepository accountsRepository) 
    : RequestHandler<AccountsGetAllRequest, AccountDto[]>(logger)
{
    protected override async Task<AccountDto[]> DoOnHandle(AccountsGetAllRequest request)
    {
        return await accountsRepository.GetByOwner(request.AuthorizedUserId);
    }
}