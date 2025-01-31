using DailyNotesChart.Persistance.Context;
using DailyNotesChart.Persistance.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DailyNotesChart.Persistance;

public static class PersistanceServicesRegistration
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<DatabaseOptionsSetup>();

        services.AddDbContext<DailyNotesChartDbContext>(dbContextOptionsBuilder =>
        {
            DatabaseOptions options = GetDatabaseOptions(configuration);

            dbContextOptionsBuilder.UseSqlServer(options.connectionString, sqlServerAction =>
            {
                sqlServerAction.EnableRetryOnFailure(options.maxRetryCount);

                sqlServerAction.CommandTimeout(options.commandTimeout);
            });

            dbContextOptionsBuilder.EnableSensitiveDataLogging(options.enableSensitiveDataLogging);

            dbContextOptionsBuilder.EnableDetailedErrors(options.enableDetailedError);
        });

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

    private record DatabaseOptions(string connectionString, int maxRetryCount, int commandTimeout, bool enableDetailedError, bool enableSensitiveDataLogging);
}