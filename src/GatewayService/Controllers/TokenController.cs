using GatewayService.JWT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GatewayService.Controllers;

/// <summary>
/// Контроллер для получения JWT-токена
/// </summary>
[Route("token")]
[ApiController]
public class TokenController : Controller
{
    private readonly List<Account> _account = new List<Account>
        {
            new Account { Login="admin", Password="admin", Role = "admin" }
        };

    /// <summary>
    /// Получение токена
    /// </summary>
    /// <param name="username">Имя пользователя</param>
    /// <param name="password">Пароль</param>
    /// <returns></returns>
    [HttpPost]
    [Route("{username}/create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult CreateToken(
        string username,
        [FromBody] string password)
    {
        var identity = GetIdentity(username, password);
        if (identity == null)
        {
            return BadRequest(new { errorText = "Invalid login or password." });
        }

        var now = DateTime.UtcNow;

        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = new
        {
            access_token = encodedJwt,
            username = identity.Name
        };

        return Json(response);
    }

    private ClaimsIdentity GetIdentity(string login, string password)
    {
        var account = _account.FirstOrDefault(x => x.Login == login && x.Password == password);
        if (account != null)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, account.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Role)
                };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        return null;
    }
}
