

using CurrencyService.BLL;
using CurrencyService.DataAccess.Repositories.CurrencyRepository;
using CurrencyService.GrpcService;
using PostgresLib;

namespace CurrencyService;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddEndpointsApiExplorer();

        serviceCollection.AddGrpcReflection();
        serviceCollection.AddGrpc();

        var connectionString = _configuration.GetConnectionString("UserCurrencyDb");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("Need to specify the connection string to UserCurrencyDb");
        }

        serviceCollection.AddSingleton<IPostgresConnectionFactory>(x =>
            new PostgresConnectionFactory(connectionString));

        serviceCollection.AddScoped<ICurrencyRepository, CurrencyRepository>();
        serviceCollection.AddScoped<IBllCurrencyService, BllCurrencyService>();

    }

    public void Configure(IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseRouting();

        applicationBuilder.UseEndpoints(
            endpointRouteBuilder =>
            {
                endpointRouteBuilder.MapGet("", () => "CurrencyService");
                endpointRouteBuilder.MapGrpcReflectionService();
                endpointRouteBuilder.MapGrpcService<GrpcCurrencyService>();
            });
    }
}
