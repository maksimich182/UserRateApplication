using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GatewayService.JWT;

public class AuthOptions
{
    public const string ISSUER = "MyServer";
    public const string AUDIENCE = "MyClient";
    const string KEY = "secretkeysecretkeysecretkeysecretkey";
    public const int LIFETIME = 1;
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}
