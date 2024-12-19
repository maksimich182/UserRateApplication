using CurrencyService.DataAccess.Repositories.Models;

namespace CurrencyService.BLL;

public interface IBllCurrencyService
{
    Task<CurrencyModel[]> GetUserCurrenciesById(int userId, CancellationToken token);
}
