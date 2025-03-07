using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.ReadModels;

namespace DailyNotesChart.Application.Operations.ChartGroups.Queries;

public sealed record GetAllChartGroupsQuery() : IQuery<List<ChartGroupReadModel>>;