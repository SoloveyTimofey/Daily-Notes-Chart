using DailyNotesChart.Domain.Exceptions;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.Exceptions;

public class PassNoteWithNonExistingChartIdInAggregateExeption : DomainException
{
    public PassNoteWithNonExistingChartIdInAggregateExeption(string message) : base (message) { }
}