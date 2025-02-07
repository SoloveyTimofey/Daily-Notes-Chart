using DailyNotesChart.Application.ReadModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DailyNotesChart.Persistance.Contexts;

public sealed class DailyNotesChartReadDbContext : DbContext
{
    public DailyNotesChartReadDbContext(DbContextOptions<DailyNotesChartReadDbContext> options) : base(options) { }

    public DbSet<ChartGroupReadModel> ChartGroups { get; set; }
    public DbSet<ChartBaseReadModel> Charts { get; set; }
    public DbSet<TimeOnlyChartReadModel> TimeOnlyCharts { get; set; }
    public DbSet<TwoDimensionalChartReadModel> TwoDimensionalCharts { get; set; }
    public DbSet<NoteTemplateReadModel> NoteTemplates { get; set; }
    public DbSet<NoteBaseReadModel> Notes { get; set; }
    public DbSet<TimeOnlyNoteReadModel> TimeOnlyNotes { get; set; }
    public DbSet<TwoDimensionalNoteReadModel> TwoDimentionalNotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            WriteConfigurationsFilter);
    }

    private static bool WriteConfigurationsFilter(Type type) =>
        type?.FullName?.Contains("Configurations.Read") ?? false;
}
