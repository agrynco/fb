namespace Services.Accounts.ResetVerified;

using DAL.Abstract.Accounts;
using DAL.Abstract.Core;
using Domain.Finances;
using Serilog;
using Services.Core;

public class AccountsResetVerifiedMessageConsumer(
    ILogger logger,
    IAccountsRepository accountsRepository,
    IUnitOfWork unitOfWork)
    : MessageConsumer<AccountsToggleVerifiedMessage>(logger)
{
    protected override async Task DoOnHandle(AccountsToggleVerifiedMessage message)
    {
        Account account = await accountsRepository.GetById(message.AccountId);

        if (account.OwnerId != message.AuthorizedUserId)
        {
            throw new WrongAccountsOwnerException(new[]
            {
                message.AccountId
            }, message.AuthorizedUserId);
        }

        account.Verified = !account.Verified;

        await new Func<Task>(async () => { await accountsRepository.Update(account); })
            .ExecuteInTransactionAsync(unitOfWork, message);
    }
}