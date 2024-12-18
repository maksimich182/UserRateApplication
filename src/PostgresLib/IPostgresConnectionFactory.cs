using Npgsql;

namespace Ozon.Route256.Practice.OrdersService.DataAccess.Postgres.Common.Single;

public interface IPostgresConnectionFactory
{
    NpgsqlConnection GetConnection();
}
