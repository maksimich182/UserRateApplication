using Npgsql;

namespace PostgresLib;

public interface IPostgresConnectionFactory
{
    NpgsqlConnection GetConnection();
}
