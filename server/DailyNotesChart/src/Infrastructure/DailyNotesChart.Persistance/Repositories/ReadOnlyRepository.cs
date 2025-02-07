using DailyNotesChart.Application.Abstractions.Persistance;
using DailyNotesChart.Application.ReadModels;
using DailyNotesChart.Persistance.Contexts;

namespace DailyNotesChart.Persistance.Repositories;

internal class ReadOnlyRepository : IReadOnlyRepository
{
    private readonly DailyNotesChartReadDbContext _context;
    public ReadOnlyRepository(DailyNotesChartReadDbContext context)
    {
        _context = context;
    }

    public IQueryable<ChartGroupReadModel> ChartGroups => _context.ChartGroups;
    public IQueryable<ChartBaseReadModel> Charts => _context.Charts;
    public IQueryable<TimeOnlyChartReadModel> TimeOnlyCharts => _context.TimeOnlyCharts;
    public IQueryable<TwoDimensionalChartReadModel> TwoDimensionalCharts => _context.TwoDimensionalCharts;
    public IQueryable<NoteTemplateReadModel> NoteTemplates => _context.NoteTemplates;
    public IQueryable<NoteBaseReadModel> Notes => _context.Notes;
    public IQueryable<TimeOnlyNoteReadModel> TimeOnlyNotes => _context.TimeOnlyNotes;
    public IQueryable<TwoDimensionalNoteReadModel> TwoDimentionalNotes => _context.TwoDimentionalNotes;
}