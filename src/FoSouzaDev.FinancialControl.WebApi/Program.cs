using FoSouzaDev.FinancialControl.Application.Services;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Factories;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using FoSouzaDev.FinancialControl.Infrastructure.Repositories;
using FoSouzaDev.FinancialControl.Infrastructure.Repositories.Generic;
using FoSouzaDev.FinancialControl.Infrastructure.Repositories.MongoDatabase;
using FoSouzaDev.FinancialControl.Infrastructure.Services;
using FoSouzaDev.FinancialControl.Infrastructure.Services.Interfaces;
using FoSouzaDev.FinancialControl.WebApi.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Newtonsoft.Json.Converters;

namespace FoSouzaDev.FinancialControl.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        Add(builder);

        WebApplication app = builder.Build();
        Use(app);
    }

    private static void Add(WebApplicationBuilder builder)
    {
        builder.Services.AddLogging(a => a.AddConsole());

        AddApplicationServices(builder.Services, builder.Configuration);

        builder.Services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
        builder.Services.AddControllers()
            .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()))
            // Utilizado pelo endpoint de POST para localizar a URL da respectiva Action
            .AddMvcOptions(options => options.SuppressAsyncSuffixInActionNames = false);
        builder.Services.AddEndpointsApiExplorer();

        AddSwagger(builder.Services);
        AddAuth(builder.Services, builder.Configuration);

        builder.Services.AddExceptionHandler<ApplicationExceptionHandler>();
        builder.Services.AddProblemDetails();

        builder.Services
            .AddHealthChecks()
            .AddMongoDb(provider => provider.GetRequiredService<IMongoClient>(), "MongoDb", HealthStatus.Unhealthy);
    }

    private static void Use(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.UseExceptionHandler();

        app.UseHealthChecks("/api/health-check");

        app.Run();
    }

    private static void AddApplicationServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IHttpResponseWriter, HttpResponseWriter>();

        services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
        services.AddSingleton<IMongoClient>(provider =>
        {
            MongoDbSettings mongoDbSettings = provider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(mongoDbSettings.ConnectionUri);
        });
        services.AddDbContext<MongoDbContext>(
            (provider, options) =>
            {
                MongoDbSettings mongoDbSettings = provider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                options.UseMongoDB(provider.GetRequiredService<IMongoClient>(), mongoDbSettings.DatabaseName);
            },
            contextLifetime: ServiceLifetime.Singleton,
            optionsLifetime: ServiceLifetime.Singleton);

        // dependentes de IHttpContextAccessor devem ser scoped pelo contexto Http ser redefinido a cada requisi��o
        // dessa forma, os outros servi�os tamb�m precisam ser definidos como Scoped
        services.AddScoped<IUserService, UserService>();

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IFinancialMovementCategoryRepository, FinancialMovementCategoryRepository>();
        services.AddScoped<IFinancialMovementRepository, FinancialMovementRepository>();
        services.AddScoped<IBankAccountRepository, BankAccountRepository>();

        services.AddScoped<IFinancialMovementCategoryFactory, FinancialMovementCategoryFactory>();
        services.AddScoped<IBankAccountFactory, BankAccountFactory>();
        services.AddScoped<IFinancialMovementFactory, FinancialMovementFactory>();

        services.AddScoped<IFinancialMovementCategoryAppService, FinancialMovementCategoryAppService>();
        services.AddScoped<IFinancialMovementAppService, FinancialMovementAppService>();
        services.AddScoped<IBankAccountAppService, BankAccountAppService>();
    }

    private static void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SchemaFilter<EnumSchemaFilter>();

            OpenApiSecurityScheme securityScheme = new()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            };

            c.AddSecurityDefinition("Bearer", securityScheme);

            OpenApiSecurityRequirement securityRequirement = new()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            };

            c.AddSecurityRequirement(securityRequirement);
        });
    }

    private static void AddAuth(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(options =>
            {
                configuration.Bind("AzureAd", options);
                options.TokenValidationParameters.NameClaimType = "name";
                options.TokenValidationParameters.ClockSkew = new TimeSpan(0, 0, 5);
                options.TokenValidationParameters.ValidateAudience = true;
                options.TokenValidationParameters.ValidateIssuer = true;
                options.TokenValidationParameters.ValidateLifetime = false;
            }, options => { configuration.Bind("AzureAd", options); });

        services.AddAuthorization(config =>
        {
            config.AddPolicy("MicrosoftIdentityPolicy", policyBuilder =>
                policyBuilder.Requirements.Add(new ScopeAuthorizationRequirement()
                {
                    RequiredScopesConfigurationKey = $"AzureAd:Scopes"
                }));
        });
    }
}