namespace DAL.Abstract.Accounts.CashAccounts;

using Domain.Finances;

public interface ICashAccountsRepository : IUserOwnedEntitiesRepository<CashAccountsGetByOwnerItem>
{
    Task<CashAccount> GetById(int id);
    Task<CashAccount> GetForUpdate(int id);
    Task Update(CashAccount account);
    Task<int> Add(CashAccount account);
}