namespace Services.Accounts.CashAccounts.GetById;

using AutoMapper;
using DAL.Abstract.Accounts.CashAccounts;
using Serilog;
using Services.Core;

public class CashAccountsGetByIdRequestHandler(
        ILogger logger,
        ICashAccountsRepository accountsRepository,
        IMapper mapper)
    : RequestHandler<CashAccountsGetByIdRequest, CashAccountsGetByIdResponse>(logger)
{
    protected override async Task<CashAccountsGetByIdResponse> DoOnHandle(CashAccountsGetByIdRequest request)
    {
        var account = await accountsRepository.GetById(request.Id);
        
        if (account.OwnerId != request.OwnerId)
        {
            throw new WrongAccountsOwnerException(new []{request.Id}, request.OwnerId);
        }
        
        return mapper.Map<CashAccountsGetByIdResponse>(account);
    }
}