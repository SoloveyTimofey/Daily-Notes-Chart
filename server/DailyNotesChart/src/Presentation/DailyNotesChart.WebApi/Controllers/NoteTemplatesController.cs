using AutoMapper;
using DailyNotesChart.Application.Operations.NoteTemplates.Commands;
using DailyNotesChart.WebApi.Requests.NoteTemplates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DailyNotesChart.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoteTemplatesController : ApplicationBaseController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public NoteTemplatesController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateNoteTemplateRequest request)
    {
        var command = _mapper.Map<CreateNoteTemplateCommand>(request);

        var result = await _sender.Send(command);

        return GetActionResult(result);
    }
}