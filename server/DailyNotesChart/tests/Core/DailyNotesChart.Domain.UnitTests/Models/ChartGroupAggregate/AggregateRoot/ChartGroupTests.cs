using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.Exceptions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster;
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

        Assert.That(secondAddingResult.IsFalure);
        Assert.That(secondAddingResult.Error, Is.EqualTo(DomainErrors.ChartGroup.CannotAddChartWithExistingDateInChartGroup));
    }

    [Test, Category("Create")]
    public void Create_PassNullParameters_CheckResult()
    {
        // Assign


        // Act
        var result = ChartGroup.Create(null!, null!);

        // Assert
        Assert.That(result.IsFalure);
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

        var noteTemplateToAdd = NoteTemplate.Create(invalidChartGroupId, Arg.Any<Color>(), Arg.Any<NoteDescription?>()).Value!;

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

    [Test, Category("AddNoteToChart")]
    public void AddNoteToChart_PassNoteWithNonExestingChartIdInAggregate_ThrowsValidException()
    {
        // Assign
        var invalidChartId = new ChartId(Guid.NewGuid());

        var noteToAdd = TimeOnlyNote.Create(invalidChartId, Arg.Any<TimeOnly>(), Arg.Any<Color>(), Arg.Any<NoteDescription>()).Value!;

        // Act && Assert
        Assert.Throws<PassNoteWithNonExistingChartIdInAggregateExeption>(
            () => _chartGroup.AddNoteToChart(noteToAdd)
        );
    }

    [Test, Category("AddNoteToChart")]
    public void AddNoteToChart_PassValidParameters_ReturnResultSuccess()
    {
        // Assign
        var validChartId = _chartGroup.Charts.First().Id;

        var noteToAdd = TimeOnlyNote.Create(validChartId, Arg.Any<TimeOnly>(), Arg.Any<Color>(), Arg.Any<NoteDescription>()).Value!;

        // Act
        var result = _chartGroup.AddNoteToChart(noteToAdd);

        // Assert
        Assert.That(result.IsSuccess);
    }

    private void PopulateChartGroupWithEntities()
    {
        _chartGroup.AddChart(
            TimeOnlyChart.Create(
                null,
                ChartDate.Create(DateOnly.FromDateTime(DateTime.UtcNow)).Value!,
                _chartGroup.Id
            ).Value!
        );
    }
}