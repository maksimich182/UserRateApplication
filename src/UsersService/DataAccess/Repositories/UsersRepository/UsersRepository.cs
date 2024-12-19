using Dapper;
using PostgresLib;

namespace UsersService.DataAccess.Repositories.UsersRepository;

public class UsersRepository : IUsersRepository
{
    private readonly IPostgresConnectionFactory _connectionFactory;

    public UsersRepository(IPostgresConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<int> CreateUserAsync(string name, CancellationToken token = default)
    {
        token.ThrowIfCancellationRequested();

        await using var connection = _connectionFactory.GetConnection();

        const string sql =
            $"""
                insert into users(name) 
                values (@name)
                returning id;
            """;

        var newUserId = await connection.ExecuteScalarAsync<int>(
            sql,
            new
            {
                name
            });

        return newUserId;
    }
}
