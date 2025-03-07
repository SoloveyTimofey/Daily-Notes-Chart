using AutoMapper;
using DailyNotesChart.Application.Operations.Account.Commands;
using DailyNotesChart.Application.Operations.Account.Queries;
using DailyNotesChart.WebApi.Requests.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DailyNotesChart.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ApplicationBaseController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public AccountController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost, Route(nameof(Register))]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        
        var result = await _sender.Send(command);

        return GetActionResult(result);
    }

    [HttpPost, Route(nameof(LoginByEmail))]
    public async Task<IActionResult> LoginByEmail([FromBody] LoginByEmailRequest request)
    {
        var query = _mapper.Map<LoginByEmailQuery>(request);

        var result = await _sender.Send(query);

        return GetActionResult(result);
    }

    [HttpPost, Route(nameof(LoginByUserName))]
    public async Task<IActionResult> LoginByUserName([FromBody] LoginByUserNameRequest request)
    {
        var query = _mapper.Map<LoginByUserNameQuery>(request);

        var result = await _sender.Send(query);

        return GetActionResult(result);
    }

    [HttpPost, Route(nameof(LoginWithRefreshToken))]
    public async Task<IActionResult> LoginWithRefreshToken([FromBody] LoginWithRefreshTokenRequest request)
    {
        var query = _mapper.Map<LoginWithRefreshTokenQuery>(request);

        var result = await _sender.Send(query);

        return GetActionResult(result);
    }
}