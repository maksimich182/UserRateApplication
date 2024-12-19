namespace GatewayService.JWT;

public record Account
{
    public required string Login { get; init; }
    public required string Password { get; init; }
    public required string Role { get; init; }
}
