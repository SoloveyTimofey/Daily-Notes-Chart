using DailyNotesChart.Domain.Models.ChartAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DailyNotesChart.Persistance.Contexts;

internal sealed class DailyNotesChartWriteDbContext : DbContext
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            WriteConfigurationsFilter);
    }

    private static bool WriteConfigurationsFilter(Type type) =>
        type?.FullName?.Contains("Configurations.Write") ?? false;
}
