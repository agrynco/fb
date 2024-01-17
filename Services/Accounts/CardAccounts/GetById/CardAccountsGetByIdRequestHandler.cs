namespace Services.Accounts.CardAccounts.GetById;

using AutoMapper;
using DAL.Abstract.Accounts.CashAccounts;
using Serilog;
using Services.Accounts.CashAccounts.GetById;
using Services.Core;

public class CardAccountsGetByIdRequestHandler(ILogger logger,
        IMapper mapper,
        ICardAccountsRepository accountsRepository)
    : RequestHandler<CardAccountsGetByIdRequest,
        CardAccountsGetByIdResponse>(logger)
{
    protected override async Task<CardAccountsGetByIdResponse> DoOnHandle(CardAccountsGetByIdRequest request)
    {
        var account = await accountsRepository.GetById(request.Id);
        
        if (account.OwnerId != request.OwnerId)
        {
            throw new WrongAccountsOwnerException(new []{request.Id}, request.OwnerId);
        }
        
        return mapper.Map<CardAccountsGetByIdResponse>(account);
    }
}