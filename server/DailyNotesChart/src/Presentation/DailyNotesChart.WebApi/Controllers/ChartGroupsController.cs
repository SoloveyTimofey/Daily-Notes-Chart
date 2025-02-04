using AutoMapper;
using DailyNotesChart.Application.Operations.ChartGroups.Commands;
using DailyNotesChart.WebApi.Requests.ChartGroups;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DailyNotesChart.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChartGroupsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    public ChartGroupsController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost] // TODO: refactor
    public async Task<IActionResult> Create(CreateChartGroupCommand command)
    {
        var result = await _sender.Send(command);

        if (result.IsSuccess)
        {
            return Ok(result.Value!.Id);
        }

        return BadRequest(result.Error);
    }

    [HttpPatch]
    public async Task<IActionResult> SetDefaultNoteTemplate(SetDefaultNoteTemplateRequest request)
    {
        var command = _mapper.Map<SetDefaultNoteTemplateCommand>(request);

        var result = await _sender.Send(command);

        if (result.IsSuccess)
        {
            return Ok();
        }

        return BadRequest(result.Error);
    }
}