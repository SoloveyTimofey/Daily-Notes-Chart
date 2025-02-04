using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;
using DailyNotesChart.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace DailyNotesChart.Persistance.Repositories;

internal class ChartGroupRepository : Repository<ChartGroup, ChartGroupId>, IChartGroupRepository
{
    public ChartGroupRepository(DailyNotesChartDbContext context) : base(context) { }

    public void CreateNoteTemplate(NoteTemplate noteTemplate)
    {
        Create<NoteTemplate, NoteTemplateId>(noteTemplate);
    }

    public void DeleteNoteTemplate(NoteTemplateId noteTemplateId)
    {
        Delete<NoteTemplate, NoteTemplateId>(noteTemplateId);
    }

    public IQueryable<ChartGroup> GetAll()
    {
        return Context.ChartGroups.AsNoTracking();
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