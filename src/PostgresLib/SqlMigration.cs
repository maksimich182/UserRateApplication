using FluentMigrator;
using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;

namespace PostgresLib;

public abstract class SqlMigration : IMigration
{
    public object ApplicationContext => throw new NotSupportedException();

    public string ConnectionString => throw new NotSupportedException();

    public void GetDownExpressions(IMigrationContext context)
    {
        _ = context ?? throw new ArgumentNullException(nameof(context));

        context.Expressions.Add(new ExecuteSqlStatementExpression { SqlStatement = GetDownSql(context.ServiceProvider) });
    }

    public void GetUpExpressions(IMigrationContext context)
    {
        _ = context ?? throw new ArgumentNullException(nameof(context));

        context.Expressions.Add(new ExecuteSqlStatementExpression { SqlStatement = GetUpSql(context.ServiceProvider) });
    }

    protected abstract string GetUpSql(IServiceProvider services);

    protected abstract string GetDownSql(IServiceProvider services);
}
