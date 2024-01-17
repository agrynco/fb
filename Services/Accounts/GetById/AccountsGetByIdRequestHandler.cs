using AutoMapper;
using DAL.Abstract.Accounts;
using Domain.Finances;
using Serilog;
using Services.Core;

namespace Services.Accounts.GetById;

public class AccountsGetByIdRequestHandler(ILogger logger,
        IAccountsRepository accountsRepository, 
        IMapper mapper)
    : RequestHandler<AccountsGetByIdRequest,
    AccountsGetByIdResponse>(logger)
{
    protected override async Task<AccountsGetByIdResponse> DoOnHandle(AccountsGetByIdRequest request)
    {
        Account account = await accountsRepository.GetById(request.Id);

        if (account.OwnerId != request.OwnerId)
        {
            throw new WrongAccountsOwnerException(new []{request.Id}, request.OwnerId);
        }

        return mapper.Map<AccountsGetByIdResponse>(account);
    }
}