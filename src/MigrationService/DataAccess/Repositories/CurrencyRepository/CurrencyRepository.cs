using Dapper;
using MigrationService.DataAccess.Models;
using PostgresLib;

namespace MigrationService.DataAccess.Repositories.CurrencyRepository;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly IPostgresConnectionFactory _connectionFactory;

    public CurrencyRepository(IPostgresConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task CreateCurrencyAsync(CurrencyModel currency, CancellationToken token = default)
    {
        token.ThrowIfCancellationRequested();

        await using var connection = _connectionFactory.GetConnection();

        const string sql =
            $"""
                insert into currency(name, rate) 
                values (@Name, @Rate);
            """;

        var result = await connection.ExecuteScalarAsync(
            sql,
            new
            {
                currency.Name,
                currency.Rate
            });
    }
}
