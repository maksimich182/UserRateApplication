using GatewayService.JWT;
using GatewayService.Providers.Currency;
using GatewayService.Providers.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.ISSUER,
                            ValidateAudience = true,
                            ValidAudience = AuthOptions.AUDIENCE,
                            ValidateLifetime = true,

                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });

        serviceCollection.AddScoped<IUsersProvider, UsersProvider>();
        serviceCollection.AddScoped<ICurrencyProvider, CurrencyProvider>();

        serviceCollection.AddControllers();
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using Bearer scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In =ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
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

        applicationBuilder.UseAuthentication();
        applicationBuilder.UseAuthorization();

        applicationBuilder.UseEndpoints(
            endpointRouteBuilder =>
            {
                endpointRouteBuilder.MapControllers();
            });
    }
}
