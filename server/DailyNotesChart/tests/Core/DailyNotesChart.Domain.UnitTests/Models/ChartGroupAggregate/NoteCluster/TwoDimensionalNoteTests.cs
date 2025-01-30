using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.Exceptions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;

namespace DailyNotesChart.Domain.UnitTests.Models.ChartGroupAggregate.NoteCluster;

[TestFixture]
public sealed class TwoDimensionalNoteTests
{
    private TwoDimentionalChart _chart;

    [SetUp]
    public void SetUp()
    {
        var yAxeValues = YAxeValues.Create(0, 10, true).Value!;

        _chart = TwoDimentionalChart.Create(
            Arg.Any<ChartSummary?>(),
            Arg.Any<ChartDate>(),
            Arg.Any<ChartGroupId>(),
            yAxeValues,
            Arg.Any<YAxeName>()
        ).Value!;
    }

    [Test]
    public void Create_PassYAxeValueThatIsOutOfChartRange_ThrowsValidException()
    {
        // Act & Assert
        Assert.Throws<SpecifiedYAxeValuOutOfRangeException>(
            () => TwoDimentionalNote.Create(
                _chart.Id,
                Arg.Any<TimeOnly>(),
                Arg.Any<Color>(),
                Arg.Any<NoteDescription>(),
                100,
                _chart
            )
        );
    }

    [Test]
    public void CreateTemplateBased_PassYAxeValueThatIsOutOfChartRange_ThrowsValidException()
    {
        // Act & Assert
        Assert.Throws<SpecifiedYAxeValuOutOfRangeException>(
            () => TwoDimentionalNote.CreateTemplateBased(
                _chart.Id,
                Arg.Any<TimeOnly>(),
                100,
                _chart,
                Arg.Any<NoteTemplate>()
            )
        );
    }
}