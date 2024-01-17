using Microsoft.EntityFrameworkCore.Storage;

namespace DAL.Abstract.Core;

public interface IUnitOfWork
{
    IDbContextTransaction BeginTransaction();
    Task<IDbContextTransaction> BeginTransactionAsync();
}