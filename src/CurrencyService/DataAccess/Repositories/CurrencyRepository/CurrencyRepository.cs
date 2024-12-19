using CurrencyService.DataAccess.Repositories.Models;
using Dapper;
using PostgresLib;

namespace CurrencyService.DataAccess.Repositories.CurrencyRepository;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly IPostgresConnectionFactory _connectionFactory;

    public CurrencyRepository(IPostgresConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<CurrencyModel[]> GetCurrenciesByUserIdAsync(int userId, CancellationToken token = default)
    {
        token.ThrowIfCancellationRequested();

        await using var connection = _connectionFactory.GetConnection();

        const string sql =
            $"""
                select 
                cur.name as Name, 
                rate as Rate
                from 
                ((users left join users_currency_link on users.id = users_currency_link.user_id)
                left join currency as cur on currency_id = cur.id)
                where users.id = @Id;
            """;

        var currencies = await connection.QueryAsync<CurrencyModel>(
            sql,
            new
            {
                Id = userId
            });

        return currencies.ToArray();
    }
}
