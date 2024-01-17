namespace Services.Accounts.BankAccounts;

using DAL.Abstract.Accounts.BankAccounts;
using Serilog;
using Services.Core;

public class BankAccountsGetByOwnerRequestHandler(
        ILogger logger,
        IBankAccountsRepository accountsRepository)
    : RequestHandler<BankAccountsGetByOwnerRequest, object>(logger)
{
    protected override async Task<object> DoOnHandle(BankAccountsGetByOwnerRequest request)
    {
        return await Task.FromResult(accountsRepository.GetByOwner(request.OwnerId, request.LoadOptions));
    }
}