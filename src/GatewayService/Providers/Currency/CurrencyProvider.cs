

using CurrencyGrpc;

namespace GatewayService.Providers.Currency;

public class CurrencyProvider : ICurrencyProvider
{
    private readonly CurrencyGrpc.Currency.CurrencyClient _client;

    public CurrencyProvider(CurrencyGrpc.Currency.CurrencyClient client)
    {
        _client = client;
    }

    public async Task<Models.CurrencyModel[]> GetUserCurrencyById(int id, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var request = new GetUserCurrencyByIdRequest { UserId = id };

        var response = await _client.GetUserCurrencyByIdAsync(request, cancellationToken: token);

        return response.Currencies.Select(ToCurrencyModel).ToArray();
    }

    private Models.CurrencyModel ToCurrencyModel(CurrencyModel currencyModel)
        => new Models.CurrencyModel
        {
            Name = currencyModel.Name,
            Rate = currencyModel.Rate
        };
}
