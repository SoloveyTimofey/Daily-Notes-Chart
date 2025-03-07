using DailyNotesChart.Domain.Shared.ResultPattern;
using Microsoft.AspNetCore.Identity;

namespace DailyNotesChart.Infrastructure.Extensions;

internal static class IdentityErrorExtensions
{
    public static Error ToError(this IdentityError identityError)
        => new Error(identityError.Code, identityError.Description);
}