using DailyNotesChart.Domain.Exceptions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.Exceptions;

public sealed class ProvidedNoteTemplateWithInvalidChartGroupIdException : DomainException
{
    public ProvidedNoteTemplateWithInvalidChartGroupIdException(ChartGroupId providedId, ChartGroupId expectedId) : base($"You tried to add NoteTemplate with chartGroupdId {providedId}, but expected {expectedId}.") { }
}