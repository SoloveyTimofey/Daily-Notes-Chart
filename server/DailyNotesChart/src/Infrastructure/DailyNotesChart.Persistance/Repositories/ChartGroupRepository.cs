using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;
using DailyNotesChart.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DailyNotesChart.Persistance.Repositories;

internal class ChartGroupRepository : Repository<ChartGroup, ChartGroupId>, IChartGroupRepository
{
    public ChartGroupRepository(DailyNotesChartWriteDbContext context) : base(context) { }

    public void CreateNoteTemplate(NoteTemplate noteTemplate)
    {
        Create<NoteTemplate, NoteTemplateId>(noteTemplate);
    }

    public void DeleteNoteTemplate(NoteTemplateId noteTemplateId)
    {
        Delete<NoteTemplate, NoteTemplateId>(noteTemplateId);
    }

    public async Task<bool> DoesChartWithSpecifiedDateExistInSpecifiedChartGroup(ChartDate chartDate, ChartGroupId chartGroupId)
    {
        return await Context.Charts
            .Where(chart => chart.ChartGroupId == chartGroupId && chart.Date == chartDate)
            .AnyAsync();
    }

    public Task<NoteTemplate?> GetNoteTemplateByIdAsync(NoteTemplateId id)
    {
        return GetByIdAsync<NoteTemplate, NoteTemplateId>(id);
    }

    public void UpdateNoteTemplate(NoteTemplate noteTemplate)
    {
        Update<NoteTemplate, NoteTemplateId>(noteTemplate);
    }
}