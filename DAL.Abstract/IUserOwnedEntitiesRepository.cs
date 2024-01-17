namespace DAL.Abstract;

using DevExtreme.AspNet.Mvc;

public interface IUserOwnedEntitiesRepository<TListItem>
{
    object GetByOwner(int ownerId, DataSourceLoadOptions loadOptions);
}