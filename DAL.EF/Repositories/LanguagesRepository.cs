namespace DAL.EF.Repositories;

using DAL.Abstract;
using DAL.EF.Core;
using Domain;
using Microsoft.EntityFrameworkCore;

public class LanguagesRepository(ApplicationDbContext applicationDbContext)
    : BaseRepository<Language>(applicationDbContext), ILanguagesRepository
{
    public async Task<Language?> GetByKey(string key)
    {
        return await EfRepository.Get(e => e.Key == key).SingleOrDefaultAsync();
    }
}