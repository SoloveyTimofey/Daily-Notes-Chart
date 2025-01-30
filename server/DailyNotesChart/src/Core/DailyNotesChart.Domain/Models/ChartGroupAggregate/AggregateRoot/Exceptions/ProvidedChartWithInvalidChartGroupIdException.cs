using DailyNotesChart.Domain.Exceptions;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.Exceptions;

public sealed class ProvidedChartWithInvalidChartGroupIdException : DomainException
{
    public ProvidedChartWithInvalidChartGroupIdException(string message) : base(message)
    {
    }
}