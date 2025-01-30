using DailyNotesChart.Domain.Exceptions;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.Exceptions;

public sealed class ProvidedNoteTemplateWithInvalidChartGroupIdException : DomainException
{
    public ProvidedNoteTemplateWithInvalidChartGroupIdException(string message) : base(message) { }
}