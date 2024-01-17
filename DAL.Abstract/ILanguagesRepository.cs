namespace DAL.Abstract;

using Domain;

public interface ILanguagesRepository
{
    Task<Language?> GetByKey(string key);
}