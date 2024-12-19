using CurrencyGrpc;

namespace GatewayService.Providers.Currency;

public interface ICurrencyProvider
{
    Task<Models.CurrencyModel[]> GetUserCurrencyById(int id, CancellationToken token);
}
