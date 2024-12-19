namespace UsersService.DataAccess.Repositories.UsersRepository;

public interface IUsersRepository
{
    Task<int> CreateUserAsync(string name, CancellationToken token = default);
}
