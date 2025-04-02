using DailyNotesChart.Application.Abstractions.Persistance;
using DailyNotesChart.Application.ReadModels;
using DailyNotesChart.Persistance.Contexts;

namespace DailyNotesChart.Persistance.Repositories;

internal class ReadOnlyRepository : IReadOnlyRepository
{
    private readonly DailyNotesChartReadDbContext _context;
    private readonly DailyNotesChartWriteDbContext _contextWrite;
    public ReadOnlyRepository(DailyNotesChartReadDbContext context, DailyNotesChartWriteDbContext contextWrite)
    {
        _context = context;
        _contextWrite = contextWrite;
    }

    //public IQueryable<ChartGroupReadModel> ChartGroups => _context.ChartGroups;
    public IQueryable<ChartGroupReadModel> ChartGroups => _contextWrite.ChartGroups.Select(wr =>new ChartGroupReadModel
        {
            Id = wr.Id.Id,
            DefaultChartTemplate = new DefaultChartTemplateReadModel(wr.DefaultChartTemplate.YAxeName.Value, wr.DefaultChartTemplate.YAxeValues.Start, wr.DefaultChartTemplate.YAxeValues.End, wr.DefaultChartTemplate.YAxeValues.IsInteger),
            DefaultNoteTemplateId = Guid.Empty,
            Name = wr.Name.Value,
            
        });
    public IQueryable<ChartBaseReadModel> Charts => _context.Charts;
    public IQueryable<TimeOnlyChartReadModel> TimeOnlyCharts => _context.TimeOnlyCharts;
    public IQueryable<TwoDimensionalChartReadModel> TwoDimensionalCharts => _context.TwoDimensionalCharts;
    public IQueryable<NoteTemplateReadModel> NoteTemplates => _context.NoteTemplates;
    public IQueryable<NoteBaseReadModel> Notes => _context.Notes;
    public IQueryable<TimeOnlyNoteReadModel> TimeOnlyNotes => _context.TimeOnlyNotes;
    public IQueryable<TwoDimensionalNoteReadModel> TwoDimensionalNotes => _context.TwoDimensionalNotes;
}