using CurrencyService.DataAccess.Repositories.CurrencyRepository;
using CurrencyService.DataAccess.Repositories.Models;

namespace CurrencyService.BLL;

public class BllCurrencyService : IBllCurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;

    public BllCurrencyService(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task<CurrencyModel[]> GetUserCurrenciesById(int userId, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var currencies = await _currencyRepository.GetCurrenciesByUserIdAsync(userId, token);

        return currencies;
    }
}
