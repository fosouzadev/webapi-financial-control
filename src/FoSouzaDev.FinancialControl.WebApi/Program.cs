using FoSouzaDev.FinancialControl.Application.Services;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Factories;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using FoSouzaDev.FinancialControl.Infrastructure.Repositories;
using FoSouzaDev.FinancialControl.Infrastructure.Services;
using FoSouzaDev.FinancialControl.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;

namespace FoSouzaDev.FinancialControl.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddLogging(a => a.AddConsole());

        AddApplicationServices(builder.Services);

        builder.Services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
        builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.Converters.Add(new StringEnumConverter()));
        builder.Services.AddEndpointsApiExplorer();

        AddSwagger(builder.Services);
        AddAuth(builder.Services, builder.Configuration);

        builder.Services.AddExceptionHandler<ApplicationExceptionHandler>();
        builder.Services.AddProblemDetails();

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.UseExceptionHandler();

        app.Run();
    }

    private static void AddApplicationServices(IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        // dependentes de IHttpContextAccessor devem ser scoped pelo contexto Http ser redefinido a cada requisição
        // dessa forma, os outros serviços também precisam ser definidos como Scoped
        services.AddScoped<IUserService, UserService>();

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
            //c.AddNewtonsoftJson();

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
                options.TokenValidationParameters.ValidateLifetime = true;
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