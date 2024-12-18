using Npgsql;
using System.Data.Common;

namespace Ozon.Route256.Practice.OrdersService.DataAccess.Postgres.Common.Single;

public static class NpgsqlExtensions
{
    public static void Add<T>(this DbParameterCollection parameters, string name, T? value) =>
        parameters.Add(
            new NpgsqlParameter<T>
            {
                ParameterName = name,
                TypedValue = value
            });
}