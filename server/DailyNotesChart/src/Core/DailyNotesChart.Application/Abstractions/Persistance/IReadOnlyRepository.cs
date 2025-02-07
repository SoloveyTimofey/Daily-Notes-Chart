using DailyNotesChart.Application.ReadModels;

namespace DailyNotesChart.Application.Abstractions.Persistance;

public interface IReadOnlyRepository
{
    IQueryable<ChartGroupReadModel> ChartGroups { get; }
    IQueryable<ChartBaseReadModel> Charts { get; }
    IQueryable<TimeOnlyChartReadModel> TimeOnlyCharts { get; }
    IQueryable<TwoDimensionalChartReadModel> TwoDimensionalCharts { get; }
    IQueryable<NoteTemplateReadModel> NoteTemplates { get; }
    IQueryable<NoteBaseReadModel> Notes { get; }
    IQueryable<TimeOnlyNoteReadModel> TimeOnlyNotes { get; }
    IQueryable<TwoDimensionalNoteReadModel> TwoDimentionalNotes { get; }
}