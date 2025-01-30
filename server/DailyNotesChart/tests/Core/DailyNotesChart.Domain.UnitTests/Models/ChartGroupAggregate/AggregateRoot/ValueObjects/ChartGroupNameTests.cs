using DailyNotesChart.Domain.Errors;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;

namespace DailyNotesChart.Domain.UnitTests.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;

[TestFixture]
public sealed class ChartGroupNameTests
{
    [Test]
    public void Create_PassTooShortName_ReturnsValidError()
    {
        // Assign
        var tooShortName = new string('a', ChartGroupName.NAME_MIN_LENGHT - 1);

        // Act
        var result = ChartGroupName.Create(tooShortName);

        // Assert
        Assert.That(result.Error, Is.EqualTo(DomainErrors.ChartGroup.InvalidChartGroupName));
    }

    [Test]
    public void Create_PassTooLongName_ReturnsValidError()
    {
        // Assign
        var tooShortName = new string('a', ChartGroupName.NAME_MAX_LENGHT + 1);

        // Act
        var result = ChartGroupName.Create(tooShortName);

        // Assert
        Assert.That(result.Error, Is.EqualTo(DomainErrors.ChartGroup.InvalidChartGroupName));
    }
}