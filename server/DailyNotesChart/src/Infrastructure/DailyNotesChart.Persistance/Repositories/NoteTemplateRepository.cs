using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;
using DailyNotesChart.Persistance.Context;

namespace DailyNotesChart.Persistance.Repositories;

internal class NoteTemplateRepository : Repository<NoteTemplate, NoteTemplateId>, INoteTemplateRepository
{
    public NoteTemplateRepository(DailyNotesChartDbContext context) : base(context) { }
}