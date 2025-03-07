using DailyNotesChart.Domain.Models.ChartAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;
using DailyNotesChart.Persistance.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DailyNotesChart.Persistance.Contexts;

internal sealed class DailyNotesChartWriteDbContext : IdentityDbContext<ApplicationUser, IdentityRole<ApplicationUserId>, ApplicationUserId>
{
    public DailyNotesChartWriteDbContext(DbContextOptions<DailyNotesChartWriteDbContext> options) : base(options) { }

    public DbSet<ChartGroup> ChartGroups { get; set; }
    public DbSet<ChartBase> Charts { get; set; }
    public DbSet<TimeOnlyChart> TimeOnlyCharts { get; set; }
    public DbSet<TwoDimentionalChart> TwoDimensionalCharts { get; set; }
    public DbSet<NoteTemplate> NoteTemplates { get; set; }
    public DbSet<NoteBase> Notes { get; set; }
    public DbSet<TimeOnlyNote> TimeOnlyNotes { get; set; }
    public DbSet<TwoDimentionalNote> TwoDimentionalNotes { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            WriteConfigurationsFilter);

        base.OnModelCreating(modelBuilder);
    }

    private static bool WriteConfigurationsFilter(Type type) =>
        type?.FullName?.Contains("Configurations.Write") ?? false;
}
