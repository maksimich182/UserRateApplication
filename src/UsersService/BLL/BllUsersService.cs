using System.Transactions;
using UsersService.DataAccess.Repositories.Models;
using UsersService.DataAccess.Repositories.UsersCurrencyLinkRepository;
using UsersService.DataAccess.Repositories.UsersRepository;

namespace UsersService.BLL;

public class BllUsersService : IBllUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUsersCurrencyLinkRepository _usersCurrencyLinkRepository;

    public BllUsersService(
        IUsersRepository usersRepository,
        IUsersCurrencyLinkRepository usersCurrencyLinkRepository)
    {
        _usersRepository = usersRepository;
        _usersCurrencyLinkRepository = usersCurrencyLinkRepository;
    }

    public async Task CreateUser(UserModel user, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var transactionOptions = new TransactionOptions()
        {
            IsolationLevel = IsolationLevel.RepeatableRead,
            Timeout = TimeSpan.FromMinutes(1)
        };

        using var transaction = new TransactionScope(
            TransactionScopeOption.Required,
            transactionOptions,
            TransactionScopeAsyncFlowOption.Enabled);

        var newUserId = await _usersRepository.CreateUserAsync(user.Name, token);

        foreach (var currencyId in user.CurrenciesIds)
        {
            await _usersCurrencyLinkRepository.CreateUsersCurrencyLink(newUserId, currencyId, token);
        }

        transaction.Complete();
    }
}
