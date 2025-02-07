using AutoMapper;
using DailyNotesChart.Application.Operations.Charts.Commands;
using DailyNotesChart.Application.Operations.Charts.Queries;
using DailyNotesChart.WebApi.Requests.Charts;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace DailyNotesChart.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChartsController : ApplicationBaseController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ChartsController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet(nameof(GetChartsByChartGroupId))]
    public async Task<IActionResult> GetChartsByChartGroupId(Guid chartGroupdId)
    {
        var query = new GetAllChartsForSpecifiedChartGroupQuery(chartGroupdId);

        var result = await _sender.Send(query);

        return GetActionResult(result);
    }

    [HttpPost(nameof(CreateTimeOnly))]
    public async Task<IActionResult> CreateTimeOnly(CreateTimeOnlyChartRequest request)
    {
        var command = _mapper.Map<CreateTimeOnlyChartCommand>(request);

        var result = await _sender.Send(command);

        return GetActionResult(result);
    }

    [HttpPost(nameof(CreateTwoDimensional))]
    public async Task<IActionResult> CreateTwoDimensional(CreateTwoDimensionalChartRequest request)
    {
        var command = _mapper.Map<CreateTwoDimensionalChartCommand>(request);

        var result = await _sender.Send(command);

        return GetActionResult(result);
    }
}