using DailyNotesChart.Domain.Models.ChartAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.ValueObjects;

namespace DailyNotesChart.Domain.UnitTests.Models.ChartGroupAggregate.ChartCluster;

public abstract class ChartTestBase
{
    public abstract void AddNote_PassDuplicateNote_ReturnsValidError();
    public abstract void AddNote_PassNoteWithInvalidType_ThrowsValidException();

    protected abstract void AddNotes();

    protected TwoDimensionalChart CreateTwoDimensionalChart() =>
        TwoDimensionalChart.Create(
            Arg.Any<ChartSummary>(),
            Arg.Any<ChartDate>(),
            Arg.Any<ChartGroupId>(),
            YAxeValues.Create(0, 10, true).Value!,
            Arg.Any<YAxeName>()
        ).Value!;

    protected TwoDimensionalNote CreateTwoDimensionalNote(ChartId chartId, TwoDimensionalChart twoDimentionalChart) =>
        TwoDimensionalNote.Create(
            chartId,
            Arg.Any<TimeOnly>(),
            Arg.Any<Color>(),
            Arg.Any<NoteDescription>(),
            twoDimentionalChart.YAxeValues.End,
            twoDimentionalChart
        ).Value!;
}