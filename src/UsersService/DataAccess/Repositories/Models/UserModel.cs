namespace UsersService.DataAccess.Repositories.Models;

public record UserModel
{
    public required string Name { get; init; }
    public required int[] CurrenciesIds { get; init; }
}
