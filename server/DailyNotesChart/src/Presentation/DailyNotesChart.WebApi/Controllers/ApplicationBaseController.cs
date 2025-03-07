using DailyNotesChart.Domain.Shared.ResultPattern;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

namespace DailyNotesChart.WebApi.Controllers;

public abstract class ApplicationBaseController : ControllerBase
{
    protected IActionResult GetActionResult<TReturnValue>(Result<TReturnValue> result)
    {
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }

    protected IActionResult GetActionResult(Result result)
    {
        if (result.IsSuccess)
        {
            return Ok();
        }

        return BadRequest(result.Errors);
    }
}