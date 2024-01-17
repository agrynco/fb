namespace Services.Accounts.CashAccounts.Create;

using AutoMapper;
using DAL.Abstract.Accounts.CashAccounts;
using Domain.Finances;
using Serilog;
using Services.Core;

public class CashAccountsCreateRequestHandler(
        ILogger logger,
        ICashAccountsRepository accountsRepository,
        IMapper mapper)
    : RequestHandler<CashAccountsCreateRequest, int>(logger)
{
    protected override async Task<int> DoOnHandle(CashAccountsCreateRequest request)
    {
        var account = mapper.Map<CashAccount>(request);
        account.Type = AccountType.Cash;
        account.OwnerId = request.AuthorizedUserId;

        return await accountsRepository.Add(account);
    }
}