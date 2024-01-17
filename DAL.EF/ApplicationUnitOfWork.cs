using DAL.EF.Core;

namespace DAL.EF;

public class ApplicationUnitOfWork : UnitOfWork<ApplicationDbContext>
{
    public ApplicationUnitOfWork(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}