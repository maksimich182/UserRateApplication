using GatewayService.Providers.Users.Models;
using UsersGrpc;

namespace GatewayService.Providers.Users;

public class UsersProvider : IUsersProvider
{
    private readonly UsersGrpc.Users.UsersClient _client;

    public UsersProvider(UsersGrpc.Users.UsersClient client)
    {
        _client = client;
    }

    public async Task CreateUser(UserModel user, CancellationToken token)
    {
        var request = new CreateUserRequest()
        {
            Name = user.Name,
            CurrenciesIds = { user.CurrenciesIds }
        };

        await _client.CreateUserAsync(request, cancellationToken: token);
    }
}
