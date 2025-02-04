using AutoMapper;
using DailyNotesChart.Application.Operations.ChartGroups.Commands;
using DailyNotesChart.Application.Operations.ChartGroups.Queries;
using DailyNotesChart.WebApi.Requests.ChartGroups;

namespace DailyNotesChart.WebApi.Profiles;

public class ChartGroupRequestsToCommandsProfile : Profile
{
    public ChartGroupRequestsToCommandsProfile()
    {
        CreateMap<SetDefaultNoteTemplateRequest, SetDefaultNoteTemplateCommand>();
    }
}