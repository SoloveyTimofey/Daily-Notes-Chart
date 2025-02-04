using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.DTOs.ChartGroups;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;

namespace DailyNotesChart.Application.Operations.ChartGroups.Queries;

public sealed record GetAllChartGroupsQuery() : IQuery<List<ChartGroupReadDto>>;