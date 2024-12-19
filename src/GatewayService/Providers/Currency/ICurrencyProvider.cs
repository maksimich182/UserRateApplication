namespace GatewayService.Providers.Currency;

public interface ICurrencyProvider
{
    Task GetUserCurrencyById(int id, CancellationToken token);
}
