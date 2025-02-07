using DailyNotesChart.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DailyNotesChart.WebApi.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<DailyNotesChartWriteDbContext>();

        dbContext.Database.Migrate();
    }
}