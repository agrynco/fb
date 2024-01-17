namespace Services.Accounts.GetWideById;

using DAL.Abstract.Accounts;
using Serilog;
using Services.Core;

public class AccountsGetWideByIdRequestHandler(
    ILogger logger,
    IAccountsRepository accountsRepository)
    : RequestHandler<AccountsGetWideByIdRequest, WideAccountDto>(logger)
{
    protected override async Task<WideAccountDto> DoOnHandle(AccountsGetWideByIdRequest request)
    {
        WideAccountDto wideAccountDto = await accountsRepository.GetWideById(request.Id);

        if (wideAccountDto.OwnerId != request.AuthenticatedUserId)
        {
            throw new WrongAccountsOwnerException(new[]
            {
                request.Id
            }, request.AuthenticatedUserId);
        }

        return wideAccountDto;
    }
}