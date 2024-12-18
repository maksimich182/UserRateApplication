using MigrationService.BackgroundJobs;
using MigrationService.DataAccess.Repositories.CurrencyRepository;
using Ozon.Route256.Practice.OrdersService.DataAccess.Postgres.Common.Single;
using System.Reflection;

namespace MigrationService;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ICurrencyRepository, CurrencyRepository>();

        services.AddEndpointsApiExplorer();
        services.AddHostedService<CurrencyFiller>();

        var connectionString = _configuration.GetConnectionString("UserConcurrencyDb");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("Need to specify the connection string to UserConcurrencyDb");
        }

        services.AddFluentMigrator(connectionString, Assembly.GetExecutingAssembly());
        services.AddSingleton<IPostgresConnectionFactory>(x =>
            new PostgresConnectionFactory(connectionString));
    }

    public void Configure(IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseRouting();
        applicationBuilder.UseEndpoints(
            endpointRouteBuilder =>
            {
                endpointRouteBuilder.MapGet("", () => "MigrationService");
            });
    }
}
