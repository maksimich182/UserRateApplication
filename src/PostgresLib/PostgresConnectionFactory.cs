using Npgsql;

namespace PostgresLib;

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
