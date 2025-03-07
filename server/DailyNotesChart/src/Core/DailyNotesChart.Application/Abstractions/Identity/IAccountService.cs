using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Application.Abstractions.Identity;

public interface IAccountService
{
    Task<Result<ApplicationUserId>> RegisterAsync(string userName, string email, string password);
    Task<Result> CheckIfPasswordValidByUserNameAsync(string userName, string password);
    Task<Result> CheckIfPasswordValidByEmailAsync(string email, string password);
    Task<Result<ApplicationUserId>> GetIdByUserName(string userName);
    Task<Result<ApplicationUserId>> GetIdByUserEmail(string email);

    /// <returns>New token if check successfull: refresh token exists and not expired</returns>
    Task<Result<(string accessToken, string refreshToken)>> CheckRefreshTokenAsync(string refreshToken);
}