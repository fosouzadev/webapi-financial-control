
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

namespace FoSouzaDev.FinancialControl.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApi(options =>
        {
            builder.Configuration.Bind("AzureAd", options);
            options.TokenValidationParameters.NameClaimType = "name";
            options.TokenValidationParameters.ClockSkew = new TimeSpan(0, 0, 5);
            options.TokenValidationParameters.ValidateAudience = true;
            options.TokenValidationParameters.ValidateIssuer = true;
            options.TokenValidationParameters.ValidateLifetime = true;
        }, options => { builder.Configuration.Bind("AzureAd", options); });

        builder.Services.AddAuthorization(config =>
        {
            config.AddPolicy("AuthZPolicy", policyBuilder =>
                policyBuilder.Requirements.Add(new ScopeAuthorizationRequirement() { RequiredScopesConfigurationKey = $"AzureAd:Scopes" }));
        });

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}