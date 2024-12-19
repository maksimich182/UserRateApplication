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

    /// <summary>
    /// Получение курсов валют по id пользователя
    /// </summary>
    /// <param name="id">Id пользователя</param>
    /// <param name="token">Токен отмены</param>
    /// <returns>Курсы валют</returns>
    [HttpGet]
    [Route("{userId}/currencies")]
    public async Task<IActionResult> GetUserCurrencyById(int userId, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var response = await _currencyProvider.GetUserCurrencyById(userId, token);

        return Json(response);
    }
}
