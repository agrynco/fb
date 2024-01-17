namespace Services.Accounts.CardAccounts.GetByOwner;

using DAL.Abstract.Accounts.CashAccounts;
using Serilog;
using Services.Core;

public class CardAccountsGetByOwnerRequestHandler(
        ILogger logger,
        ICardAccountsRepository accountsRepository)
    : RequestHandler<CardAccountsGetByOwnerRequest, object>(logger)
{
    protected override async Task<object> DoOnHandle(CardAccountsGetByOwnerRequest request)
    {
        return await Task.FromResult(accountsRepository.GetByOwner(request.OwnerId, request.LoadOptions));
    }
}