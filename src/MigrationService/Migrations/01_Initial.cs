using FluentMigrator;
using PostgresLib;

namespace MigrationService.Migrations;

[Migration(1, "Initial migration")]
public class Initial : SqlMigration
{
    protected override string GetDownSql(IServiceProvider services)
        => """
            drop table if exists users_currency_link;
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

        create table if not exists users_currency_link(
            id serial primary key,
            user_id integer references users(id) not null,
            currency_id integer references currency(id) not null
        );
        """;
}
