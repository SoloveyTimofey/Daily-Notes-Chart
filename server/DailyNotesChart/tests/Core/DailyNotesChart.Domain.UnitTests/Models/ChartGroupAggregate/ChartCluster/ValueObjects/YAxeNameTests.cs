using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;

namespace DailyNotesChart.Domain.UnitTests.Models.ChartGroupAggregate.ChartCluster.ValueObjects;

[TestFixture]
public sealed class YAxeNameTests
{
    [Test]
    public void Create_PassTooShortName_ReturnsValidError()
    {
        // Assign
        var tooShortName = new string('a', YAxeName.NAME_MIN_LENGHT - 1);

        // Act
        var result = YAxeName.Create(tooShortName);

        // Assert
        Assert.Contains(DomainErrors.Chart.InvalidYAxeName, result.Errors.ToList());
    }

    [Test]
    public void Create_PassTooLongName_ReturnsValidError()
    {
        // Assign
        var tooLongName = new string('a', YAxeName.NAME_MAX_LENGHT + 1);

        // Act
        var result = YAxeName.Create(tooLongName);

        // Assert
        Assert.Contains(DomainErrors.Chart.InvalidYAxeName, result.Errors.ToList());
    }
}