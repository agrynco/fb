using DAL.Abstract.Core;
using Microsoft.EntityFrameworkCore.Storage;
using Services.Exceptions;
using Services.Transactions.Create.Transfer;

namespace Services;

public static class InTransactionExecutionUtils
{
    public static async Task<TResult> ExecuteInTransactionAsync<TResult>(this Func<Task<TResult>> func,
        IUnitOfWork unitOfWork, ITransactional transactional)
    {
        IDbContextTransaction? existedDbContextTransaction = transactional.DbContextTransaction;
        
        IDbContextTransaction dbContextTransaction =
            existedDbContextTransaction ?? await unitOfWork.BeginTransactionAsync();
        
        transactional.DbContextTransaction = dbContextTransaction;

        try
        {
            TResult result = await func();

            if (existedDbContextTransaction == null)
            {
                await dbContextTransaction.CommitAsync();
            }

            return result;
        }
        catch (Exception e)
        {
            if (existedDbContextTransaction == null)
            {
                await dbContextTransaction.RollbackAsync();
            }

            throw new ServiceException(e.Message, e);
        }
        finally
        {
            if (existedDbContextTransaction == null)
            {
                await dbContextTransaction.DisposeAsync();
            }
        }
    }
    
    public static async Task ExecuteInTransactionAsync(this Func<Task> func,
        IUnitOfWork unitOfWork, ITransactional transactional)
    {
        IDbContextTransaction? existedDbContextTransaction = transactional.DbContextTransaction;
        
        IDbContextTransaction dbContextTransaction =
            existedDbContextTransaction ?? await unitOfWork.BeginTransactionAsync();
        
        transactional.DbContextTransaction = dbContextTransaction;
        
        try
        {
            await func();

            if (existedDbContextTransaction == null)
            {
                await dbContextTransaction.CommitAsync();
            }
        }
        catch (Exception e)
        {
            if (existedDbContextTransaction == null)
            {
                await dbContextTransaction.RollbackAsync();
            }

            throw new ServiceException(e.Message, e);
        }
        finally
        {
            if (existedDbContextTransaction == null)
            {
                await dbContextTransaction.DisposeAsync();
            }
        }
    }
}