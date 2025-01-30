using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;

namespace DailyNotesChart.Domain.UnitTests.Models.ChartGroupAggregate.ChartCluster.ValueObjects;

[TestFixture]
public sealed class YAxeValuesTests
{
    [Test]
    public void Create_PassDoubleAndIsIntegerTrue_ParsesToInt()
    {
        // Assign
        var start = -2.5;
        var end = 2.5;

        // Act
        var resultValue = YAxeValues.Create(start, end, true).Value!;

        // Assert
        Assert.That(resultValue.Start % 1 == 0);
        Assert.That(resultValue.End % 1 == 0);
    }

    [Test]
    public void Create_PassTooSmallValue_ReturnsValidError()
    {
        // Assign
        var tooSmallValue = YAxeValues.MIN_VALUE - 1;

        // Act
        var result = YAxeValues.Create(tooSmallValue, YAxeValues.MAX_VALUE, true);

        // Assert
        Assert.That(result.Error, Is.EqualTo(DomainErrors.Chart.ValuesOutOfRange));
    }

    [Test]
    public void Create_PassTooBigValue_ReturnsValidError()
    {
        // Assign
        var tooBigValue = YAxeValues.MAX_VALUE + 1;

        // Act
        var result = YAxeValues.Create(YAxeValues.MIN_VALUE, tooBigValue, true);

        // Assert
        Assert.That(result.Error, Is.EqualTo(DomainErrors.Chart.ValuesOutOfRange));
    }

    [Test]
    public void Create_PassStartValueThatIsGreaterThanEndValue_ReturnsValidError()
    {
        // Assign
        var startValue = YAxeValues.MAX_VALUE;
        var endValue = YAxeValues.MAX_VALUE - 1;

        // Act
        var result = YAxeValues.Create(startValue, endValue, true);

        // Assert
        Assert.That(result.Error, Is.EqualTo(DomainErrors.Chart.StartValueGreaterThanEndValue));
    }
}