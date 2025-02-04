using AutoMapper;
using DailyNotesChart.Application.Operations.NoteTemplates.Commands;
using DailyNotesChart.WebApi.Requests.NoteTemplates;

namespace DailyNotesChart.WebApi.Profiles;

public class NoteTemplateRequestsToCommandsProfile : Profile
{
    public NoteTemplateRequestsToCommandsProfile()
    {
        CreateMap<CreateNoteTemplateRequest, CreateNoteTemplateCommand>();
    }
}