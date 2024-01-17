namespace Services.Accounts.CardAccounts.Update;

using AutoMapper;
using DAL.Abstract.Accounts.CashAccounts;
using DAL.Abstract.Transaction;
using Domain.Finances;
using Serilog;
using Services.Core;

public class CardAccountsUpdateMessageConsumer(ILogger logger,
        ICardAccountsRepository accountsRepository,
        ITransactionsRepository transactionsRepository,
        IMapper mapper)
    : MessageConsumer<CardAccountsUpdateMessage>(logger)
{
    protected override async Task DoOnHandle(CardAccountsUpdateMessage message)
    {
        CardAccount account = await accountsRepository.GetById(message.Id);

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