using GatewayService.Providers.Users.Models;

namespace GatewayService.Providers.Users;

public interface IUsersProvider
{
    Task CreateUser(UserModel user, CancellationToken token);
}
