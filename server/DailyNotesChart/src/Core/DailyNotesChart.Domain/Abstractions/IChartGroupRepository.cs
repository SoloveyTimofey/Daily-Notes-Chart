using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;

namespace DailyNotesChart.Domain.Abstractions;

public interface IChartGroupRepository : ICrudRepository<ChartGroup, ChartGroupId>
{
    IQueryable<ChartGroup> GetAll();

    void CreateNoteTemplate(NoteTemplate noteTemplate);
    void UpdateNoteTemplate(NoteTemplate noteTemplate);
    void DeleteNoteTemplate(NoteTemplateId noteTemplateId);
    Task<NoteTemplate?> GetNoteTemplateByIdAsync(NoteTemplateId id);
}