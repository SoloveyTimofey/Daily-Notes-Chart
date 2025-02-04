using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;

namespace DailyNotesChart.Domain.Abstractions;

public interface INoteTemplateRepository : ICrudRepository<NoteTemplate, NoteTemplateId>
{
}