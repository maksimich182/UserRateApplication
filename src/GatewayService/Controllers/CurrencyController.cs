using GatewayService.Providers.Currency;
using Microsoft.AspNetCore.Mvc;

namespace GatewayService.Controllers;

/// <summary>
/// Контроллер для взаимодействия с CurrencyService
/// </summary>
[Route("currency")]
[ApiController]
public class CurrencyController : Controller
{
    private readonly ICurrencyProvider _currencyProvider;

    public CurrencyController(ICurrencyProvider currencyProvider)
    {
        _currencyProvider = currencyProvider;
    }


}
