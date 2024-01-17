namespace DAL.Abstract.Files;

using Domain;

public interface IFilesRepository
{
    Task Remove(int id);
    Task<int> Add(File file);
}