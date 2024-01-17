namespace DAL.EF.Repositories.Files;

using DAL.Abstract.Files;
using DAL.EF.Core;
using Domain;

public class FilesRepository(ApplicationDbContext applicationDbContext)
    : BaseRepository<File>(applicationDbContext), IFilesRepository
{
    public async Task Remove(int id)
    {
        await EfRepository.DeleteAsync(id);
    }

    public async Task<int> Add(File file)
    {
        return (await EfRepository.AddAsync(file)).Id; 
    }
}