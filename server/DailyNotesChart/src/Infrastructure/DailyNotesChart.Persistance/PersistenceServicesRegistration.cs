using DailyNotesChart.Application.Abstractions.Persistance;
using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Persistance.Contexts;
using DailyNotesChart.Persistance.InternalAbstractions;
using DailyNotesChart.Persistance.Models;
using DailyNotesChart.Persistance.Repositories;
using DailyNotesChart.Persistance.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DailyNotesChart.Persistance;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DailyNotesChartWriteDbContext>(dbContextOptionsBuilder =>
        {
            DatabaseOptions options = GetDatabaseOptions(configuration);

            dbContextOptionsBuilder.UseSqlServer(options.ConnectionString, sqlServerAction =>
            {
                sqlServerAction.EnableRetryOnFailure(options.MaxRetryCount);
                sqlServerAction.CommandTimeout(options.CommandTimeout);
            });

            dbContextOptionsBuilder.EnableSensitiveDataLogging(options.EnableSensitiveDataLogging);
            dbContextOptionsBuilder.EnableDetailedErrors(options.EnableDetailedError);
        });

        services.AddDbContext<DailyNotesChartReadDbContext>(dbContextOptionsBuilder =>
        {
            DatabaseOptions options = GetDatabaseOptions(configuration);

            dbContextOptionsBuilder.UseSqlServer(options.ConnectionString, sqlServerAction =>
            {
                sqlServerAction.EnableRetryOnFailure(options.MaxRetryCount);
                sqlServerAction.CommandTimeout(options.CommandTimeout);
            });

            dbContextOptionsBuilder.EnableSensitiveDataLogging(options.EnableSensitiveDataLogging);
            dbContextOptionsBuilder.EnableDetailedErrors(options.EnableDetailedError);
            dbContextOptionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole<ApplicationUserId>>()
            .AddEntityFrameworkStores<DailyNotesChartWriteDbContext>()
            .AddDefaultTokenProviders();
        
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 5;
            options.Password.RequireNonAlphanumeric = false;
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var secret = configuration["JwtConfiguration:Secret"];
            var issuer = configuration["JwtConfiguration:ValidIssuer"];
            var audience = configuration["JwtConfiguration:ValidAudiences"];
            if (secret is null || issuer is null || audience is null)
            {
                throw new ApplicationException("Jwt is not set in the configuration");
            }
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidIssuer = issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
            };
        });

        services.AddScoped<IChartGroupRepository, ChartGroupRepository>();
        services.AddScoped<IChartRepository, ChartRepository>();
        services.AddScoped<IReadOnlyRepository, ReadOnlyRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IEnsureDbPopulatedWithNecessaryData, EnsureDbPopulatedWithIdentityData>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        return services;
    }

    private static DatabaseOptions GetDatabaseOptions(IConfiguration configuration)
    {
        var dbOptionsSection = configuration.GetRequiredSection("DatabaseOptions");

        string connectionString = configuration.GetConnectionString("DailyNotesChartConnection")!;
        int maxRetryCount = int.Parse(dbOptionsSection.GetRequiredSection("MaxRetryCount").Value!);
        int commandTimeout = int.Parse(dbOptionsSection.GetRequiredSection("CommandTimeout").Value!);
        bool enableDetailedError = bool.Parse(dbOptionsSection.GetRequiredSection("EnableDetailedError").Value!);
        var enableSensitieDataLogging = bool.Parse(dbOptionsSection.GetRequiredSection("EnableSensitieDataLogging").Value!);

        return new DatabaseOptions(connectionString, maxRetryCount, commandTimeout, enableDetailedError, enableSensitieDataLogging);
    }

    private record DatabaseOptions(string ConnectionString, int MaxRetryCount, int CommandTimeout, bool EnableDetailedError, bool EnableSensitiveDataLogging);
}