namespace DAL.Abstract.Accounts.BankAccounts;

using Domain.Finances;

public interface IBankAccountsRepository : IUserOwnedEntitiesRepository<BankAccountsGetByOwnerItem>
{
    Task<BankAccount> GetById(int id);
    Task Update(BankAccount account);
    Task<int> Add(BankAccount account);
}