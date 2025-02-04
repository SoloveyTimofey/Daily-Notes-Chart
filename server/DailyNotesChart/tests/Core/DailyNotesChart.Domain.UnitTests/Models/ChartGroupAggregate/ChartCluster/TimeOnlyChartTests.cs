using DailyNotesChart.Domain.Models.ChartAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.Exceptions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.ValueObjects;

namespace DailyNotesChart.Domain.UnitTests.Models.ChartGroupAggregate.ChartCluster;

[TestFixture]
public sealed class TimeOnlyChartTests : ChartTestBase
{
    private TimeOnlyChart _chart;

    [SetUp]
    public void SetUp()
    {
        _chart = TimeOnlyChart.Create(
            Arg.Any<ChartSummary>(),
            Arg.Any<ChartDate>(),
            Arg.Any<ChartGroupId>()
        ).Value!;

        AddNotes();
    }

    [Test]
    public override void AddNote_PassDuplicateNote_ReturnsValidError()
    {
        // Assign
        var duplicateTime = _chart.Notes.First().Time;

        var duplicateNote = TimeOnlyNote.Create(
            chartId: _chart.Id,
            duplicateTime,
            Arg.Any<Color>(),
            Arg.Any<NoteDescription>()
        ).Value!;

        // Act
        var result = _chart.AddNote(duplicateNote);

        // Assert
        Assert.That(result.IsFailure);
        Assert.That(result.Error, Is.EqualTo(DomainErrors.Chart.CannotAddNoteWithDuplicateCoordinates));
    }

    [Test]
    public override void AddNote_PassNoteWithInvalidType_ThrowsValidException()
    {
        // Assign
        var twoDimensionalChart = CreateTwoDimensionalChart();

        var twoDimensionalNote = CreateTwoDimensionalNote(_chart.Id, twoDimensionalChart);

        // Act & Assert
        Assert.Throws<SpecifiedInvalidNoteTypeForChartException>(
            () => _chart.AddNote(twoDimensionalNote)
        );
    }

    protected override void AddNotes()
    {
        var noteToAdd = TimeOnlyNote.Create(
            chartId: _chart.Id,
            Arg.Any<TimeOnly>(),
            Arg.Any<Color>(),
            Arg.Any<NoteDescription>()
        ).Value!;

        _chart.AddNote(noteToAdd);
    }
}