using Domain.Finances;

namespace DAL.Abstract.Currencies;

public interface ICurrenciesRepository
{
    Task<Currency[]> GetAll();
}