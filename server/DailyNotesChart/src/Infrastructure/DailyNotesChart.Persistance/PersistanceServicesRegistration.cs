using DailyNotesChart.Application.Abstractions.Persistance;
using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Persistance.Context;
using DailyNotesChart.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DailyNotesChart.Persistance;

public static class PersistanceServicesRegistration
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DailyNotesChartDbContext>((Action<DbContextOptionsBuilder>?)(dbContextOptionsBuilder =>
        {
            DatabaseOptions options = GetDatabaseOptions(configuration);

            dbContextOptionsBuilder.UseSqlServer((string)options.ConnectionString, (Action<Microsoft.EntityFrameworkCore.Infrastructure.SqlServerDbContextOptionsBuilder>)(sqlServerAction =>
            {
                sqlServerAction.EnableRetryOnFailure((int)options.MaxRetryCount);

                sqlServerAction.CommandTimeout(options.CommandTimeout);
            }));

            dbContextOptionsBuilder.EnableSensitiveDataLogging(options.EnableSensitiveDataLogging);

            dbContextOptionsBuilder.EnableDetailedErrors(options.EnableDetailedError);
        }));

        services.AddScoped<IChartGroupRepository, ChartGroupRepository>();
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