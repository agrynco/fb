using AutoMapper;
using DAL.Abstract.Accounts;
using DAL.Abstract.Transaction;
using Domain.Finances;
using Serilog;
using Services.Core;

namespace Services.Accounts.Update;

using DAL.Abstract.Accounts.CashAccounts;

public class CashAccountsUpdateMessageConsumer(ILogger logger, ICashAccountsRepository accountsRepository,
        ITransactionsRepository transactionsRepository, IMapper mapper)
    : MessageConsumer<CashAccountsUpdateMessage>(logger)
{
    protected override async Task DoOnHandle(CashAccountsUpdateMessage message)
    {
        CashAccount account = await accountsRepository.GetById(message.Id);

        if (account.OwnerId != message.AuthorizedUserId)
        {
            throw new WrongAccountsOwnerException(new []{message.Id}, message.AuthorizedUserId);
        }

        if (account.CurrencyId != message.CurrencyId &&
            await transactionsRepository.AreAnyTransactionWithAccounts(new []{message.Id}))
        {
            throw new AccountChangeCurrencyException(message.Id, message.CurrencyId);
        }

        account = mapper.Map(message, account);
        account.OwnerId = message.AuthorizedUserId;

        await accountsRepository.Update(account);
    }
}