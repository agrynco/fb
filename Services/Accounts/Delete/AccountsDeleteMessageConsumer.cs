namespace Services.Accounts.Delete;

using DAL.Abstract.Accounts;
using DAL.Abstract.Core;
using DAL.Abstract.Transaction;
using Serilog;
using Services.Core;

public class AccountsDeleteMessageConsumer(
        ILogger logger,
        IAccountsRepository accountsRepository,
        ITransactionsRepository transactionsRepository,
        IUnitOfWork unitOfWork)
    : MessageConsumer<AccountsDeleteMessage>(logger)
{
    protected override async Task DoOnHandle(AccountsDeleteMessage message)
    {
        int[] ownerIds = await accountsRepository.GetOwnerIds(message.Ids);

        if (ownerIds.Length > 1 || ownerIds[0] != message.AuthorizedUserId)
        {
            throw new WrongAccountsOwnerException(message.Ids, message.AuthorizedUserId);
        }

        if (await transactionsRepository.AreAnyTransactionWithAccounts(message.Ids))
        {
            throw new AccountsDeleteException(message.Ids);
        }

        await new Func<Task>(async () =>
        {
            foreach (int id in message.Ids)
            {
                await accountsRepository.Delete(id);
            }
        }).ExecuteInTransactionAsync(unitOfWork, message);
    }
}