using DailyNotesChart.Domain.Exceptions;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.Exceptions;

public sealed class SpecifiedInvalidNoteTypeForChartException : DomainException
{
    public SpecifiedInvalidNoteTypeForChartException(Type chartType, Type requiredNoteType)
        : base($"You specified invalid note type. Chart of type {chartType} requires {requiredNoteType} note type.")
    {
    }
}
