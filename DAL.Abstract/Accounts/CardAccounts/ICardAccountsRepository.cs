namespace DAL.Abstract.Accounts.CashAccounts;

using Domain.Finances;

public interface ICardAccountsRepository : IUserOwnedEntitiesRepository<CardAccountsGetByOwnerItem>
{
    Task<CardAccount> GetById(int id);
    Task Update(CardAccount account);
    Task<int> Add(CardAccount account);
}