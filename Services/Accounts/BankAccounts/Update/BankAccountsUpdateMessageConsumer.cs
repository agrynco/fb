namespace Services.Accounts.BankAccounts.Update;

using AutoMapper;
using DAL.Abstract.Accounts.BankAccounts;
using DAL.Abstract.Transaction;
using Domain.Finances;
using Serilog;
using Services.Accounts.CardAccounts.Update;
using Services.Core;

public class BankAccountsUpdateMessageConsumer(ILogger logger,
        IBankAccountsRepository accountsRepository,
        ITransactionsRepository transactionsRepository,
        IMapper mapper)
    : MessageConsumer<BankAccountsUpdateMessage>(logger)
{
    protected override async Task DoOnHandle(BankAccountsUpdateMessage message)
    {
        BankAccount account = await accountsRepository.GetById(message.Id);

        if (account.OwnerId != message.AuthorizedUserId)
        {
            throw new WrongAccountsOwnerException(new[]
            {
                message.Id
            }, message.AuthorizedUserId);
        }

        if (account.CurrencyId != message.CurrencyId &&
            await transactionsRepository.AreAnyTransactionWithAccounts(new[]
            {
                message.Id
            }))
        {
            throw new AccountChangeCurrencyException(message.Id, message.CurrencyId);
        }

        account = mapper.Map(message, account);

        await accountsRepository.Update(account);
    }
}