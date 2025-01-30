using DailyNotesChart.Domain.Exceptions;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.Exceptions;

public sealed class SpecifiedYAxeValuOutOfRangeException : DomainException
{
    public SpecifiedYAxeValuOutOfRangeException(string message) : base(message) { }
}