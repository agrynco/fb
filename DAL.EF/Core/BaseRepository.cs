namespace DAL.EF.Core;

using Domain;

public abstract class BaseRepository<TEntity> : IDisposable where TEntity : Entity
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly Lazy<EfRepository<TEntity>> _lazyEfRepository;
    private bool _disposed;

    protected BaseRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _lazyEfRepository = new Lazy<EfRepository<TEntity>>(CreateRepository);
    }

    protected EfRepository<TEntity> EfRepository => _lazyEfRepository.Value;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private EfRepository<TEntity> CreateRepository()
    {
        return new EfRepository<TEntity>(_applicationDbContext);
    }

    protected virtual void DoDispose()
    {
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _applicationDbContext.Dispose();

            if (_lazyEfRepository.IsValueCreated)
            {
                EfRepository.Dispose();
            }

            DoDispose();
        }

        _disposed = true;
    }
}