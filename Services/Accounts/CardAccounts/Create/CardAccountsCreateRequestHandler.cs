namespace Services.Accounts.CardAccounts.Create;

using AutoMapper;
using DAL.Abstract.Accounts.CashAccounts;
using Domain.Finances;
using Serilog;
using Services.Core;

public class CardAccountsCreateRequestHandler(
        ILogger logger,
        ICardAccountsRepository accountsRepository,
        IMapper mapper)
    : RequestHandler<CardAccountsCreateRequest, int>(logger)
{
    protected override async Task<int> DoOnHandle(CardAccountsCreateRequest request)
    {
        var account = mapper.Map<CardAccount>(request);
        account.Type = AccountType.Card;
        account.OwnerId = request.AuthorizedUserId;
        
        return await accountsRepository.Add(account);
    }
}