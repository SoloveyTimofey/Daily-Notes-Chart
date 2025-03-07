using DailyNotesChart.Domain.Shared.ResultPattern;
using Microsoft.AspNetCore.Identity;

namespace DailyNotesChart.Infrastructure.Extensions;

internal static class IdentityResultExtensions
{
    public static Result<TFailureResult> ToFailureResult<TFailureResult>(this IdentityResult identityResult)
       => Result.Failure<TFailureResult>(
           [.. identityResult.Errors.Select(x => x.ToError())]
       );
}