namespace DAL.EF.Repositories;

using DAL.Abstract;
using DAL.EF.Core;
using Domain;

public class GeoLocationPositionsRepository(ApplicationDbContext applicationDbContext)
    : BaseRepository<GeoLocationPosition>(applicationDbContext), IGeoLocationPositionsRepository
{
    public async Task Delete(int id)
    {
        await EfRepository.DeleteAsync(id);
    }
}