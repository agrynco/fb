using DAL.Abstract.Currencies;
using DAL.EF.Core;
using Domain.Finances;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories.Currencies;

public class CurrenciesRepository(ApplicationDbContext dbContext)
    : BaseRepository<Currency>(dbContext), ICurrenciesRepository
{
    public async Task<Currency[]> GetAll()
    {
        return await EfRepository.Get().ToArrayAsync();
    }
}