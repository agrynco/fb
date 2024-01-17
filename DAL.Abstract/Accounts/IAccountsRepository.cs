using Domain.Finances;

namespace DAL.Abstract.Accounts;

using DevExtreme.AspNet.Mvc;

public interface IAccountsRepository : IUserOwnedEntitiesRepository<AccountsGetByOwnerItem>
{
    Task<Account> GetById(int id);
    Task<AccountDto[]> GetByIds(int[] ids);
    Task Update(Account account);
    Task<Account> GetForUpdate(int id);
    Task Delete(int id);
    Task<int> Add(Account account);
    Task<int[]> GetOwnerIds(int[] ids);
    Task<AccountDto[]> GetByOwner(int ownerId);
    Task<object> GetWideByOwnerId(int ownerId, DataSourceLoadOptions loadOptions);
    Task<WideAccountDto> GetWideById(int id);
}