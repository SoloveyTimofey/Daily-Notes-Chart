using DailyNotesChart.Application.Abstractions.Persistance;
using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Persistance.Contexts;
using DailyNotesChart.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DailyNotesChart.Persistance;

public static class PersistanceServicesRegistration
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
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

        services.AddScoped<IChartGroupRepository, ChartGroupRepository>();
        services.AddScoped<IChartRepository, ChartRepository>();
        services.AddScoped<IReadOnlyRepository, ReadOnlyRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

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