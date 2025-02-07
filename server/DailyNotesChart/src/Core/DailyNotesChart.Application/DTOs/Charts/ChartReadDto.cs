using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.Enums;
using DailyNotesChart.Application.ReadModels;

namespace DailyNotesChart.Application.DTOs.Charts;

public abstract record ChartReadDto(
    ChartType ChartType,
    Guid Id,
    DateOnly Date,
    Guid ChartGroupId,
    List<NoteBaseReadModel> Notes    
);

public sealed record TimeOnlyChartReadDto : ChartReadDto
{
    public TimeOnlyChartReadDto(
        Guid id,
        DateOnly date,
        Guid chartGroupId,
        List<NoteBaseReadModel> notes) : base(ChartType.TimeOnly, id, date, chartGroupId, notes)
    {
    }
}

public sealed record TwoDimensionalChartReadDto : ChartReadDto
{
    public TwoDimensionalChartReadDto(
        Guid id,
        DateOnly date,
        Guid chartGroupId,
        List<NoteBaseReadModel> notes,
        YAxeValuesReadModel yAxeValues,
        string yAxeName) : base(ChartType.TwoDimensioanl, id, date, chartGroupId, notes)
    {
        YAxeValues = yAxeValues;
        YAxeName = yAxeName;
    }

    public YAxeValuesReadModel YAxeValues {  get; set; }
    public string YAxeName { get; set; } 
}