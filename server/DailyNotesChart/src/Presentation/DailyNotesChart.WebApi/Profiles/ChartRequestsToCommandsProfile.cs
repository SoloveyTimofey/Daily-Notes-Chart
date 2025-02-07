using AutoMapper;
using DailyNotesChart.Application.Operations.Charts.Commands;
using DailyNotesChart.WebApi.Requests.Charts;

namespace DailyNotesChart.WebApi.Profiles;

public class ChartRequestsToCommandsProfile : Profile
{
    public ChartRequestsToCommandsProfile()
    {
        CreateMap<CreateTimeOnlyChartRequest, CreateTimeOnlyChartCommand>();
        CreateMap<CreateTwoDimensionalChartRequest, CreateTwoDimensionalChartCommand>();
    }
}