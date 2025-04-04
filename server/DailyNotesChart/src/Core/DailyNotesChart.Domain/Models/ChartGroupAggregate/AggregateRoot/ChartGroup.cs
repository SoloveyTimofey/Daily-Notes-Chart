using DailyNotesChart.Domain.Errors;
using DailyNotesChart.Domain.Models.ChartAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.Exceptions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;
using DailyNotesChart.Domain.Primitives;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;

public sealed class ChartGroup : AggregateRoot<ChartGroupId>
{
    private List<ChartBase> _charts = new();
    private List<NoteTemplate> _noteTemplates = new();
    #pragma warning disable
    private ChartGroup() { } // EF Core constructor
    #pragma warning enable
    private ChartGroup(
        ChartGroupId id,
        ChartGroupName name,
        List<ChartBase> charts,
        List<NoteTemplate> noteTemplates,
        NoteTemplate? defaultNoteTemplate,
        DefaultChartTemplate defaultChartTemplate,
        ApplicationUserId creatorId) : base(id)
    {
        Name = name;
        _charts = charts;
        _noteTemplates = noteTemplates;
        DefaultNoteTemplate = defaultNoteTemplate;
        DefaultChartTemplate = defaultChartTemplate;
        CreatorId = creatorId;
    }

    public ChartGroupName Name { get; private set; }

    public IReadOnlyCollection<ChartBase> Charts => _charts;
    public IReadOnlyCollection<NoteTemplate> NoteTemplates => _noteTemplates;

    public NoteTemplateId? DefaultNoteTemplateId { get; private set; }
    public NoteTemplate? DefaultNoteTemplate { get; private set; }
    public ApplicationUserId CreatorId { get; }

    public DefaultChartTemplate DefaultChartTemplate { get; private set; }

    public static Result<ChartGroup> Create(ChartGroupName name, ApplicationUserId creatorId, DefaultChartTemplate? defaultChartTemplate = null)
    {
        if (defaultChartTemplate is null)
        {
            var yAxeName = YAxeName.CreateDefault();
            var yAxeValues = YAxeValues.CreateDefault();

            defaultChartTemplate = new DefaultChartTemplate(yAxeName, yAxeValues);
        }

        return Result.Success(
            new ChartGroup(
                id: new ChartGroupId(Guid.NewGuid()),
                name,
                charts: [],
                noteTemplates: [],
                defaultNoteTemplate: null,
                defaultChartTemplate,
                creatorId
            )
        );
    }

    public Result SetDefaultNoteTemplate(NoteTemplate noteTemplate)
    {
        if (noteTemplate.ChartGroupId != Id)
        {
            throw new ProvidedNoteTemplateWithInvalidChartGroupIdException(noteTemplate.ChartGroupId, Id);
        }

        DefaultNoteTemplate = noteTemplate;

        return Result.Success();
    }


    /// <exception cref="ProvidedChartWithInvalidChartGroupIdException"/>
    public Result AddChart(ChartBase chartToAdd)
    {
        // Business rule: do not add chart with existing date in chart group
        if (_charts.Any(currentChart => currentChart.Date == chartToAdd.Date))
        {
            return Result.Failure(DomainErrors.ChartGroup.CannotAddChartWithExistingDateInChartGroup);
        }

        if (chartToAdd.ChartGroupId != Id)
        {
            throw new ProvidedChartWithInvalidChartGroupIdException($"You tried to add Chart with chartGroupId {chartToAdd.ChartGroupId}, but expected {Id}.");
        }

        _charts.Add(chartToAdd);

        return Result.Success();
    }

    /// <exception cref="ProvidedNoteTemplateWithInvalidChartGroupIdException"></exception>
    public Result AddNoteTemplate(NoteTemplate noteTemplateToAdd)
    {
        if (noteTemplateToAdd.ChartGroupId != Id)
        {
            throw new ProvidedNoteTemplateWithInvalidChartGroupIdException(noteTemplateToAdd.ChartGroupId, Id);
        }

        _noteTemplates.Add(noteTemplateToAdd);

        return Result.Success();
    }

    // TODO: Impelement other kinds of operations on Entities
}
