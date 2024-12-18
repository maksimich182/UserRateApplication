using MigrationService.DataAccess.Models;

namespace MigrationService.DataAccess.Repositories.CurrencyRepository;

public interface ICurrencyRepository
{
    Task CreateCurrencyAsync(CurrencyModel currency, CancellationToken token = default);
}
