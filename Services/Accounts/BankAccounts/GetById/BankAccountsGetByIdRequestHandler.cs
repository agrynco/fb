namespace Services.Accounts.BankAccounts.GetById;

using AutoMapper;
using DAL.Abstract.Accounts.BankAccounts;
using Serilog;
using Services.Core;

public class BankAccountsGetByIdRequestHandler(ILogger logger,
        IMapper mapper,
        IBankAccountsRepository accountsRepository)
    : RequestHandler<BankAccountsGetByIdRequest,
        BankAccountsGetByIdResponse>(logger)
{
    protected override async Task<BankAccountsGetByIdResponse> DoOnHandle(BankAccountsGetByIdRequest request)
    {
        var account = await accountsRepository.GetById(request.Id);
        
        if (account.OwnerId != request.OwnerId)
        {
            throw new WrongAccountsOwnerException(new []{request.Id}, request.OwnerId);
        }
        
        return mapper.Map<BankAccountsGetByIdResponse>(account);
    }
}