using DailyNotesChart.Application.Abstractions.Persistance;
using DailyNotesChart.Application.Operations.ChartGroups.Commands;
using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Domain.Errors;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;

namespace DailyNotesChart.Application.UnitTests.Operations.ChartGroups.Commands;

[TestFixture]
public sealed class CreateChartGroupCommandHandlerTests
{
    private IChartGroupRepository _chartGroupRepository;
    private IUnitOfWork _unitOfWork;
    private readonly string _validName = "My Daily Efficiency";

    [SetUp]
    public void SetUp()
    {
        _chartGroupRepository = Substitute.For<IChartGroupRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
    }

    [Test, Category("Name")]
    public async Task Handle_PassTooLongChartGroupName_ReturnsValidError()
    {
        // Arrange
        string tooLongName = new string('a', ChartGroupName.NAME_MAX_LENGHT + 1);

        var command = new CreateChartGroupCommand(tooLongName, Arg.Any<Guid>());

        var handler = new CreateChartGroupCommandHandler(_chartGroupRepository, _unitOfWork);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.That(result.Errors, Is.EqualTo(DomainErrors.ChartGroup.InvalidChartGroupName));
    }

    [Test, Category("Name")]
    public async Task Handle_PassTooShortChartGroupName_ReturnsValidError()
    {
        // Arrange
        string tooShortName = new string('a', ChartGroupName.NAME_MIN_LENGHT - 1);

        var command = new CreateChartGroupCommand(tooShortName, Arg.Any<Guid>());

        var handler = new CreateChartGroupCommandHandler(_chartGroupRepository, _unitOfWork);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.That(result.Errors, Is.EqualTo(DomainErrors.ChartGroup.InvalidChartGroupName));
    }

    [Test, Category("Name")]
    public async Task Handle_PassValidName_ReturnsResultSuccess()
    {
        // Arrange
        var command = new CreateChartGroupCommand(_validName, Arg.Any<Guid>());

        var handler = new CreateChartGroupCommandHandler(_chartGroupRepository, _unitOfWork);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.That(result.IsSuccess);
    }

    [Test, Category("DefaultChartTemplate")]
    public async Task Handle_PassInvalidDefaultChartTemplate_ReturnsError()
    {
        // Arrange
        var invalidDefaultChartTemplate = new DTOs.ChartGroups.CreateDefaultChartTemplateDto(
            YAxeName: "Some yAxeName",
            Start: 20,
            End: 10,
            IsInteger: true
        );

        var command = new CreateChartGroupCommand(_validName, Arg.Any<Guid>(), invalidDefaultChartTemplate);

        var handler = new CreateChartGroupCommandHandler(_chartGroupRepository, _unitOfWork);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.That(result.IsFailure);
    }

    [Test, Category("NoteTemplate")]
    public async Task Handle_PassInvalidDefaultNoteTemplateTemplate_ReturnsError()
    {
        // Arrange
        var invalidDefaultNoteTemplate = new DTOs.ChartGroups.CreateNoteTemplateDto(
            Color: "Invalid Color",
            NoteDescription: "My Note"
        );

        var command = new CreateChartGroupCommand(_validName, Arg.Any<Guid>(), null, invalidDefaultNoteTemplate);

        var handler = new CreateChartGroupCommandHandler(_chartGroupRepository, _unitOfWork);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.That(result.IsFailure);
    }

    [Test]
    public async Task Handle_PassValidParameters_ReturnsExpectedResult()
    {
        // Arrange
        var validChartTemplate = new DTOs.ChartGroups.CreateDefaultChartTemplateDto
        (
            YAxeName: "Values",
            Start: 0,
            End: 10,
            IsInteger: true
        );

        var validNoteTemplate = new DTOs.ChartGroups.CreateNoteTemplateDto
        (
            Color: "#FFFFFF",
            NoteDescription: "Woke up"
        );

        var command = new CreateChartGroupCommand(_validName, Arg.Any<Guid>(), validChartTemplate, validNoteTemplate);

        var handler = new CreateChartGroupCommandHandler( _chartGroupRepository, _unitOfWork);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.That(result.IsSuccess);

        var chartGroup = result.Value!;
    }
}