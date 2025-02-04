using DailyNotesChart.Domain.Models.ChartAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.Exceptions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;

namespace DailyNotesChart.Domain.UnitTests.Models.ChartGroupAggregate.AggregateRoot;

[TestFixture]
public sealed class ChartGroupTest
{
    private ChartGroup _chartGroup;

    [SetUp]
    public void SetUp()
    {
        var chartGroupName = ChartGroupName.Create("Test Chart Group").Value;
        var defaultChartTemplate = new DefaultChartTemplate(
                YAxeName: YAxeName.Create("Y Axe").Value!,
                YAxeValues: YAxeValues.Create(-10, 10, true).Value!
        );

        var createChartGroupResult = ChartGroup.Create(
            chartGroupName!,
            defaultChartTemplate!
        );

        _chartGroup = createChartGroupResult.Value!;

        PopulateChartGroupWithEntities();
    }

    [Test, Category("AddChart")]
    public void AddChart_PassChartWithExistingDateInChartGroup_ReturnsValidError()
    {
        // Assign
        var repeatedDate = ChartDate.Create(new DateOnly(2022, 2, 2)).Value!;

        var firstChartToAdd = TimeOnlyChart.Create(Arg.Any<ChartSummary>(), repeatedDate, _chartGroup.Id).Value!;
        var secondChartToAdd = TimeOnlyChart.Create(Arg.Any<ChartSummary>(), repeatedDate, _chartGroup.Id).Value!;

        // Act
        var firstAddingResult = _chartGroup.AddChart(firstChartToAdd);
        var secondAddingResult = _chartGroup.AddChart(firstChartToAdd);

        // Assert
        Assert.That(firstAddingResult.IsSuccess);

        Assert.That(secondAddingResult.IsFailure);
        Assert.That(secondAddingResult.Error, Is.EqualTo(DomainErrors.ChartGroup.CannotAddChartWithExistingDateInChartGroup));
    }

    [Test, Category("AddChart")]
    public void AddChart_PassChartWithInvalidChartGroupId_ThrowsValidException()
    {
        // Assign
        var invalidChartGroupId = new ChartGroupId(Guid.NewGuid());

        var chartToAdd = TimeOnlyChart.Create(Arg.Any<ChartSummary>(), Arg.Any<ChartDate>(), invalidChartGroupId).Value!;

        // Act & Assert
        Assert.Throws<ProvidedChartWithInvalidChartGroupIdException>(
            () => _chartGroup.AddChart(chartToAdd)
        );
    }

    [Test, Category("AddChart")]
    public void AddChart_PassValidParameters_ReturnsResultSuccess()
    {
        // Assign
        var validSummary = ChartSummary.Create("Today was a good day.").Value!;
        var validChartDate = ChartDate.Create(DateOnly.FromDateTime(DateTime.UtcNow)).Value!;
        var validChartGroupId = _chartGroup.Id;

        var chartToAdd = TimeOnlyChart.Create(validSummary, validChartDate, validChartGroupId).Value!;

        // Act
        var result = _chartGroup.AddChart(chartToAdd);

        // Assert
        Assert.That(result.IsSuccess);
        Assert.That(_chartGroup.Charts, Does.Contain(chartToAdd));
    }

    [Test, Category("AddNoteTemplate")]
    public void AddNoteTemplate_PassNoteTemplateWithInvalidChartGroupId_ThrowsValidException()
    {
        // Assign
        var invalidChartGroupId = new ChartGroupId(Guid.NewGuid());

        var noteTemplateToAdd = NoteTemplate.Create(invalidChartGroupId, Arg.Any<Color>(), Arg.Any<NoteDescription>()).Value!;

        // Act & Assert
        Assert.Throws<ProvidedNoteTemplateWithInvalidChartGroupIdException>(
            () => _chartGroup.AddNoteTemplate(noteTemplateToAdd)
        );
    }

    [Test, Category("AddNoteTemplate")]
    public void AddNoteTemplate_PassValidParameters_ReturnsResultSuccess()
    {
        // Assign
        var validChartGroupId = _chartGroup.Id;
        var validColor = Color.Create("#FFFFFF").Value!;
        var validDescription = NoteDescription.Create("High productivity.").Value!;

        var noteTemplateToAdd = NoteTemplate.Create(validChartGroupId, validColor, validDescription).Value!;

        // Act
        var result = _chartGroup.AddNoteTemplate(noteTemplateToAdd);

        // Assert
        Assert.That(result.IsSuccess);
        Assert.That(_chartGroup.NoteTemplates, Does.Contain(noteTemplateToAdd));
    }

    private void PopulateChartGroupWithEntities()
    {
        _chartGroup.AddChart(
            TimeOnlyChart.Create(
                Arg.Any<ChartSummary>(),
                ChartDate.Create(DateOnly.FromDateTime(DateTime.UtcNow)).Value!,
                _chartGroup.Id
            ).Value!
        );
    }
}