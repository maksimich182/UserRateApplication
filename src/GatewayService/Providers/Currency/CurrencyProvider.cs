

namespace GatewayService.Providers.Currency;

public class CurrencyProvider : ICurrencyProvider
{
    private readonly CurrencyGrpc.Currency.CurrencyClient _client;

    public CurrencyProvider(CurrencyGrpc.Currency.CurrencyClient client)
    {
        _client = client;
    }

    public Task GetUserCurrencyById(int id, CancellationToken token)
    {
        
    }
}
