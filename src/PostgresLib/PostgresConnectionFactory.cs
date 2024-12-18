using Npgsql;

namespace Ozon.Route256.Practice.OrdersService.DataAccess.Postgres.Common.Single;

public class PostgresConnectionFactory : IPostgresConnectionFactory
{
    private readonly string _connectionString;

    public PostgresConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public NpgsqlConnection GetConnection()
        => new NpgsqlConnection(_connectionString);
}
