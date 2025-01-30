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
        Assert.That(result.Error, Is.EqualTo(DomainErrors.Chart.InvalidChartSummary));
    }

    [Test]
    public void Create_PassTooShortSummary_ReturnsValidError()
    {
        // Assign
        var tooShortSummary = new string('a', ChartSummary.SUMMARY_MIN_LENGHT - 1);

        // Act
        var result = ChartSummary.Create(tooShortSummary);

        // Assert
        Assert.That(result.Error, Is.EqualTo(DomainErrors.Chart.InvalidChartSummary));
    }
}
