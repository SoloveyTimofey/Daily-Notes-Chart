using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Application.Abstractions.Identity;

public interface ITokenProvider
{
    Task<Result<string>> GenerateAccessTokenForUserByEmailAsync(string userEmail);
    Task<Result<string>> GenerateAccessTokenForUserByUserNameAsync(string userName);

    /// <summary>
    /// Generates refresh token and saves it in the db
    /// </summary>
    Task<string> GenerateRefreshTokenForUserAsync(ApplicationUserId userId);
}