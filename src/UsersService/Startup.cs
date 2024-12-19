using PostgresLib;
using UsersService.BLL;
using UsersService.DataAccess.Repositories.UsersCurrencyLinkRepository;
using UsersService.DataAccess.Repositories.UsersRepository;
using UsersService.GrpcService;

namespace UsersService;

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

        var connectionString = _configuration.GetConnectionString("UserConcurrencyDb");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("Need to specify the connection string to UserConcurrencyDb");
        }

        serviceCollection.AddSingleton<IPostgresConnectionFactory>(x =>
            new PostgresConnectionFactory(connectionString));

        serviceCollection.AddScoped<IUsersCurrencyLinkRepository, UsersCurrencyLinkRepository>();
        serviceCollection.AddScoped<IUsersRepository, UsersRepository>();
        serviceCollection.AddScoped<IBllUsersService, BllUsersService>();

    }

    public void Configure(IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseRouting();

        applicationBuilder.UseEndpoints(
            endpointRouteBuilder =>
            {
                endpointRouteBuilder.MapGet("", () => "UsersService");
                endpointRouteBuilder.MapGrpcReflectionService();
                endpointRouteBuilder.MapGrpcService<GrpcUsersService>();
            });
    }
}
