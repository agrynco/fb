using System.Linq.Expressions;
using DAL.Abstract.Core;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Core;

public abstract class BaseEfRepository<TDbContext, TEntity, TEntityId> :
    IEfRepository<TEntity, TEntityId>
    where TEntity : class, IEntity<TEntityId>
    where TDbContext : DbContext
    where TEntityId : struct
{
    private bool _disposed;

    protected BaseEfRepository(TDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<TEntity>();
    }

    public TDbContext DbContext { get; }

    private DbSet<TEntity> DbSet { get; }

    public TEntity Add(TEntity entity, bool save = true)
    {
        var entityEntry = DbSet.Add(entity);
        TEntity addedEntity = entityEntry.Entity;

        if (save)
        {
            DbContext.SaveChanges();
        }

        return addedEntity;
    }

    public void Add(TEntity[] entities, bool save = true)
    {
        foreach (TEntity entity in entities)
        {
            Add(entity, save);
        }
    }

    public async Task<TEntity> AddAsync(TEntity entity, bool save = true)
    {
        var entityEntry = await DbSet.AddAsync(entity);
        TEntity addedEntity = entityEntry.Entity;

        if (save)
        {
            await DbContext.SaveChangesAsync();
        }

        return addedEntity;
    }

    public void Delete(IEnumerable<TEntityId> id)
    {
        foreach (TEntityId entityId in id)
        {
            Delete(new[]
            {
                GetById(entityId)!
            });
        }
    }

    public void Delete(TEntityId id)
    {
        Delete(new[]
        {
            id
        });
    }

    public virtual void Delete(TEntity[] entities)
    {
        DoRemove(entities);
        DbContext.SaveChanges();
    }

    public void Delete(TEntity entity)
    {
        DoRemove(entity);
        DbContext.SaveChanges();
    }

    public async Task DeleteAsync(TEntityId id)
    {
        Delete(id);
        await DbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(IEnumerable<TEntityId> id)
    {
        foreach (TEntityId entityId in id)
        {
            await DeleteAsync(entityId);
        }
    }

    public async Task DeleteAsync(TEntity entity)
    {
        DoRemove(entity);
        await DbContext.SaveChangesAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(TEntityId id)
    {
        TEntity? entity = await DbSet.FindAsync(id);

        return entity;
    }

    public async Task UpdateAsync(TEntity entity, bool save = true)
    {
        DbSet.Attach(entity);
        DbContext.Entry(entity).State = EntityState.Modified;
        if (save)
        {
            await DbContext.SaveChangesAsync();
        }
    }

    public virtual void Update(TEntity entity, bool save = true)
    {
        DbSet.Attach(entity);
        DbContext.Entry(entity).State = EntityState.Modified;

        if (save)
        {
            DbContext.SaveChanges();
        }
    }

    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = DbContext.Set<TEntity>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (string includeProperty in includeProperties.Split(new[]
                         {
                             ','
                         },
                         StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        if (orderBy != null)
        {
            return orderBy(query).AsQueryable();
        }

        return query.AsQueryable();
    }

    public virtual IQueryable<TEntity> GetAll()
    {
        return DbContext.Set<TEntity>().AsQueryable();
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await DbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<bool> Exists(TEntityId id)
    {
        return await GetAll().Where(e => Equals(e.Id, id)).AnyAsync();
    }

    public virtual TEntity? GetById(TEntityId id)
    {
        return DbContext.Set<TEntity>().Find(id);
    }

    public void ClearChangeTracker()
    {
        DbContext.ChangeTracker.Clear();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void DoRemove(TEntity[] entities)
    {
        foreach (TEntity entity in entities)
        {
            DoRemove(entity);
        }
    }

    protected virtual void DoRemove(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            DbContext.Dispose();
        }

        _disposed = true;
    }
}

public class EfRepository<TEntity> : BaseEfRepository<ApplicationDbContext, TEntity, int>
    where TEntity : class, IEntity<int>
{
    public EfRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}