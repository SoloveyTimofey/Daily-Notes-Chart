using DailyNotesChart.Application.ReadModels;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Persistance.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DailyNotesChart.Persistance.Contexts;

internal sealed class DailyNotesChartReadDbContext : IdentityDbContext<ApplicationUser, IdentityRole<ApplicationUserId>, ApplicationUserId>
{
    public DailyNotesChartReadDbContext(DbContextOptions<DailyNotesChartReadDbContext> options) : base(options) { }

    //public DbSet<ChartGroupReadModel> ChartGroups { get; set; }
    public DbSet<ChartBaseReadModel> Charts { get; set; }
    public DbSet<TimeOnlyChartReadModel> TimeOnlyCharts { get; set; }
    public DbSet<TwoDimensionalChartReadModel> TwoDimensionalCharts { get; set; }
    public DbSet<NoteTemplateReadModel> NoteTemplates { get; set; }
    public DbSet<NoteBaseReadModel> Notes { get; set; }
    public DbSet<TimeOnlyNoteReadModel> TimeOnlyNotes { get; set; }
    public DbSet<TwoDimensionalNoteReadModel> TwoDimensionalNotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            ReadConfigurationsFilter);

        base.OnModelCreating(modelBuilder);
    }

    private static bool ReadConfigurationsFilter(Type type) =>
        type?.FullName?.Contains("Configurations.Read") ?? false;
}
