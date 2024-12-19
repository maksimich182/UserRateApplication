using UsersService.DataAccess.Repositories.Models;

namespace UsersService.BLL;

public interface IBllUsersService
{
    Task CreateUser(UserModel user, CancellationToken token);
}
