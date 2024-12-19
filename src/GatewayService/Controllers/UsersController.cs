using GatewayService.Providers.Users;
using GatewayService.Providers.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GatewayService.Controllers;

/// <summary>
/// Контроллер для взаимодействия с UsersService
/// </summary>
[Authorize(Roles = "admin")]
[Route("users")]
[ApiController]
public class UsersController : Controller
{
    private readonly IUsersProvider _usersProvider;

    public UsersController(IUsersProvider usersProvider)
    {
        _usersProvider = usersProvider;
    }

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="name">Имя пользователя</param>
    /// <param name="currenciesIds">Id интересующих валют</param>
    /// <param name="token">Токен отмены</param>
    [HttpPost]
    [Route("{name}/create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateUser(
        string name, 
        [FromQuery] int[] currenciesIds, 
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var user = new UserModel { Name = name, CurrenciesIds = currenciesIds };
        await _usersProvider.CreateUser(user, token);
        return Ok();
    }
}
