namespace DAL.Abstract.Banks;

using DevExtreme.AspNet.Mvc;
using Domain.Finances;

public interface IBanksRepository
{
    Task<object> GetAll(int ownerId, DataSourceLoadOptions loadOptions);
    Task<Bank?> GetById(int id);
    Task<int[]> GetOwnerIds(int[] ids);
    Task<int> Add(Bank bank);
    Task Update(Bank bank);
    Task Delete(int[] ids);
}