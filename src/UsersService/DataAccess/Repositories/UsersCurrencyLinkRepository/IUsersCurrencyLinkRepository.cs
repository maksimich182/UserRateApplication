namespace UsersService.DataAccess.Repositories.UsersCurrencyLinkRepository;

public interface IUsersCurrencyLinkRepository
{
    Task CreateUsersCurrencyLink(int userId, int currencyId, CancellationToken token = default);
}
