using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Domain.Errors;
using DailyNotesChart.Domain.Models.ChartAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Shared;

namespace DailyNotesChart.Domain.Services;

public class CreateChartService
{
    private IChartGroupRepository _chartGroupRepository;

    public CreateChartService(IChartGroupRepository chartGroupRepository)
    {
        _chartGroupRepository = chartGroupRepository;
    }

    public async Task<Result<TimeOnlyChart>> CreateTimeOnlyChartAsync(ChartSummary summary, ChartDate date, ChartGroupId chartGroupId)
    {
        if (await _chartGroupRepository.DoesChartWithSpecifiedDateExistInSpecifiedChartGroup(date, chartGroupId))
        {
            return Result.Failure<TimeOnlyChart>(DomainErrors.ChartGroup.CannotAddChartWithExistingDateInChartGroup);
        }

        var createResult = TimeOnlyChart.Create(summary, date, chartGroupId);

        return createResult;
    }

    public async Task<Result<TwoDimentionalChart>> CreateTwoDimensionalChartAsync(ChartSummary summary, ChartDate date, ChartGroupId chartGroupId, YAxeValues yAxeValues, YAxeName yAxeName)
    {
        if (await _chartGroupRepository.DoesChartWithSpecifiedDateExistInSpecifiedChartGroup(date, chartGroupId))
        {
            return Result.Failure<TwoDimentionalChart>(DomainErrors.ChartGroup.CannotAddChartWithExistingDateInChartGroup);
        }

        var createResult = TwoDimentionalChart.Create(summary, date, chartGroupId, yAxeValues, yAxeName);

        return createResult;
    }
}