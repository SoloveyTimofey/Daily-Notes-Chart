using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;

namespace DailyNotesChart.Domain.UnitTests.Models.ChartGroupAggregate.ChartCluster.ValueObjects;

[TestFixture]
public sealed class ChartSummaryTests
{
    [Test]
    public void Create_PassTooLongSummary_ReturnsValidError()
    {
        // Assign
        var tooLongSummary = new string('a', ChartSummary.SUMMARY_MAX_LENGHT + 1);

        // Act
        var result = ChartSummary.Create(tooLongSummary);

        // Assert
        Assert.Contains(DomainErrors.Chart.InvalidChartSummary, result.Errors.ToList());
    }

    [Test]
    public void Create_PassEmptySummary_ReturnsResultSuccess()
    {
        // Assign
        var emptySummary = string.Empty;

        // Act
        var result = ChartSummary.Create(emptySummary);

        // Assert
        Assert.That(result.IsSuccess);
    }
}
