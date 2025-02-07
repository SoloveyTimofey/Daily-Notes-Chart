using DailyNotesChart.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DailyNotesChart.WebApi.Controllers;

public class ApplicationBaseController : ControllerBase
{
    protected IActionResult GetActionResult<TReturnValue>(Result<TReturnValue> result)
    {
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    protected IActionResult GetActionResult(Result result)
    {
        if (result.IsSuccess)
        {
            return Ok();
        }

        return BadRequest(result.Error);
    }
}