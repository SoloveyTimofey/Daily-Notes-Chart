using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Application.Abstractions.Identity;

public interface ITokenProvider
{
    Task<Result<string>> GenerateTokenForUserByEmailAsync(string userEmail);
    Task<Result<string>> GenerateTokenForUserByUserNameAsync(string userName);

    /// <summary>
    /// Generates refresh token and saves it in the db
    /// </summary>
    Task<string> GenerateRefreshTokenForUserAsync(ApplicationUserId userId);
}