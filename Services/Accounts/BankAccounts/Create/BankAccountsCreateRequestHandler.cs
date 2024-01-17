namespace Services.Accounts.BankAccounts.Create;

using AutoMapper;
using DAL.Abstract.Accounts.BankAccounts;
using Domain.Finances;
using Serilog;
using Services.Core;

public class BankAccountsCreateRequestHandler(
        ILogger logger,
        IBankAccountsRepository accountsRepository,
        IMapper mapper)
    : RequestHandler<BankAccountsCreateRequest, int>(logger)
{
    protected override async Task<int> DoOnHandle(BankAccountsCreateRequest request)
    {
        var account = mapper.Map<BankAccount>(request);
        account.Type = AccountType.Bank;
        account.OwnerId = request.AuthorizedUserId;
        
        return await accountsRepository.Add(account);
    }
}