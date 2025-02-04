using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.Exceptions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.ValueObjects;

namespace DailyNotesChart.Domain.UnitTests.Models.ChartGroupAggregate.ChartCluster;

[TestFixture]
public sealed class TwoDimensionalChartTests : ChartTestBase
{
    private TwoDimentionalChart _chart;

    [SetUp]
    public void SetUp()
    {
        _chart = CreateTwoDimensionalChart();

        AddNotes();
    }

    [Test]
    public override void AddNote_PassDuplicateNote_ReturnsValidError()
    {
        // Assign
        var duplicateNote = CreateTwoDimensionalNote(_chart.Id, _chart);

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
        var timeOnlyChart = TimeOnlyChart.Create(
            Arg.Any<ChartSummary?>(),
            Arg.Any<ChartDate>(),
            Arg.Any<ChartGroupId>()
        ).Value!;

        var timeOnlyNote = TimeOnlyNote.Create(
            timeOnlyChart.Id,
            Arg.Any<TimeOnly>(),
            Arg.Any<Color>(),
            Arg.Any<NoteDescription>()
        ).Value!;

        // Act & Assert
        Assert.Throws<SpecifiedInvalidNoteTypeForChartException>(
            () => _chart.AddNote(timeOnlyNote)
        );
    }

    protected override void AddNotes()
    {
        var noteToAdd = CreateTwoDimensionalNote(_chart.Id, _chart);

        _chart.AddNote(noteToAdd);
    }
}