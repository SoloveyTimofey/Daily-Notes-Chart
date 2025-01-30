using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.ValueObjects;

namespace DailyNotesChart.Domain.UnitTests.Models.ChartGroupAggregate.NoteCluster.ValueObjects;

[TestFixture]
public sealed class ColorTests
{
    [Test]
    [TestCase("FFFFFF")]
    [TestCase("#FFF")]
    [TestCase("")]
    public void Create_PassInvalidColorColorFormat_ReturnsValidError(string invalidColor)
    {
        // Act
        var result = Color.Create(invalidColor);

        // Assert
        Assert.That(result.Error, Is.EqualTo(DomainErrors.NoteTemplate.InvalidColorFormat));
    }
    [Test]
    public void Create_PassValidColorColorFormat_ReturnsResultSuccess()
    {
        // Assign
        var validColor = "#FFFFFF";

        // Act
        var result = Color.Create(validColor);

        // Assert
        Assert.That(result.IsSuccess);
    }
}