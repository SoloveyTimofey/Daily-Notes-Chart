using AutoMapper;
using DailyNotesChart.Application.Operations.ChartGroups.Commands;
using DailyNotesChart.Application.Operations.ChartGroups.Queries;
using DailyNotesChart.Application.Shared;
using DailyNotesChart.WebApi.Consts;
using DailyNotesChart.WebApi.Requests.ChartGroups;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace DailyNotesChart.WebApi.Controllers;

[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class ChartGroupsController : ApplicationBaseController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    public ChartGroupsController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateChartGroupCommand command)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (command.CreatorId.ToString() != userId)
        {
            return StatusCode(StatusCodes.Status403Forbidden, ConstStrings.ResponseMessages.YouDontHavePermissionToPerformThisAction);
        }

        var result = await _sender.Send(command);

        return GetActionResult(result);
    }

    [HttpPatch(nameof(SetDefaultNoteTemplate))]
    public async Task<IActionResult> SetDefaultNoteTemplate(SetDefaultNoteTemplateRequest request)
    {
        var command = _mapper.Map<SetDefaultNoteTemplateCommand>(request);

        var result = await _sender.Send(command);

        return GetActionResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllChartGroupsQuery();

        var result = await _sender.Send(query);

        return GetActionResult(result);
    }
}