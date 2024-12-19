using CurrencyService.DataAccess.Repositories.Models;

namespace CurrencyService.DataAccess.Repositories.CurrencyRepository;

public interface ICurrencyRepository
{
    Task<CurrencyModel[]> GetCurrenciesByUserIdAsync(int userId, CancellationToken token = default);
}
