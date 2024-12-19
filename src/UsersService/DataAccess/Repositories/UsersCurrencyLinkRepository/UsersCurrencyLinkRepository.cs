using Dapper;
using PostgresLib;

namespace UsersService.DataAccess.Repositories.UsersCurrencyLinkRepository;

public class UsersCurrencyLinkRepository : IUsersCurrencyLinkRepository
{
    private readonly IPostgresConnectionFactory _connectionFactory;

    public UsersCurrencyLinkRepository(IPostgresConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task CreateUsersCurrencyLink(int userId, int currencyId, CancellationToken token = default)
    {
        token.ThrowIfCancellationRequested();

        await using var connection = _connectionFactory.GetConnection();

        const string sql =
            $"""
                insert into users_currency_link(user_id, currency_id) 
                values (@UserId, @CurrencyId);
            """;

        await connection.ExecuteScalarAsync(
        sql,
        new
        {
            UserId = userId,
            CurrencyId = currencyId
        });
    }
}
