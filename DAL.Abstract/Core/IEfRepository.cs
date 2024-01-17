using System.Linq.Expressions;
using Domain;

namespace DAL.Abstract.Core;

public interface IEfRepository : IDisposable
{
    void ClearChangeTracker();
}

public interface IEfRepository<TEntity, in TEntityId> : IEfRepository
    where TEntity : IEntity<TEntityId> where TEntityId : struct
{
    TEntity Add(TEntity entity, bool save = true);
    void Add(TEntity[] entities, bool save = true);
    Task<TEntity> AddAsync(TEntity entity, bool save = true);

    void Delete(TEntity[] entities);
    void Delete(TEntity entity);
    void Delete(IEnumerable<TEntityId> id);
    void Delete(TEntityId id);

    Task DeleteAsync(TEntityId id);
    Task DeleteAsync(IEnumerable<TEntityId> id);
    Task DeleteAsync(TEntity entity);

    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");

    IQueryable<TEntity> GetAll();

    Task<List<TEntity>> GetAllAsync();

    Task<bool> Exists(TEntityId id);

    TEntity? GetById(TEntityId id);
    Task<TEntity?> GetByIdAsync(TEntityId id);
    Task UpdateAsync(TEntity entity, bool save = true);

    void Update(TEntity entity, bool save = true);
}

public interface IEfRepository<TEntity> : IEfRepository<TEntity, int>
    where TEntity : IEntity<int>
{
}