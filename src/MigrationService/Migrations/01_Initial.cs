using FluentMigrator;
using Ozon.Route256.Practice.OrdersService.DataAccess.Postgres.Common.Single;

namespace MigrationService.Migrations;

[Migration(1, "Initial migration")]
public class Initial : SqlMigration
{
    protected override string GetDownSql(IServiceProvider services)
        => """
            drop table if exists currency;
            drop table if exists users;
        """;

    protected override string GetUpSql(IServiceProvider services)
        => """
        create table if not exists currency(
            id serial primary key,
            name text,
            rate real
        );

        create table if not exists users(
            id serial primary key,
            name text
        );
        """;
}
