using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;

namespace DailyNotesChart.Domain.Abstractions;

public interface INoteTemplateRepository
{
    /// <exception cref="ArgumentNullException"></exception>
    NoteTemplate GetById(NoteTemplateId id);
}