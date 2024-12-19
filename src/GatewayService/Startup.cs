using GatewayService.Providers.Users;
using System.Reflection;

namespace GatewayService;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddGrpcClient<UsersGrpc.Users.UsersClient>(
            options =>
            {
                var url = _configuration.GetValue<string>("USERS_ADDRESS");
                if (string.IsNullOrEmpty(url))
                {
                    throw new ArgumentException("Требуется указать переменную окружения USERS_ADDRESS или она пустая");
                }

                options.Address = new Uri(url);
            });

        serviceCollection.AddGrpcClient<CurrencyGrpc.Currency.CurrencyClient>(
            options =>
            {
                var url = _configuration.GetValue<string>("CURRENCY_ADDRESS");
                if (string.IsNullOrEmpty(url))
                {
                    throw new ArgumentException("Требуется указать переменную окружения CURRENCY_ADDRESS или она пустая");
                }

                options.Address = new Uri(url);
            });


        serviceCollection.AddScoped<IUsersProvider, UsersProvider>();

        serviceCollection.AddControllers();
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen(options =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            options.CustomSchemaIds(type => type.ToString());
        });
    }

    public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment env)
    {
        applicationBuilder.UseRouting();
        applicationBuilder.UseSwagger();
        applicationBuilder.UseSwaggerUI();
        applicationBuilder.UseEndpoints(
            endpointRouteBuilder =>
            {
                endpointRouteBuilder.MapControllers();
            });
    }
}
