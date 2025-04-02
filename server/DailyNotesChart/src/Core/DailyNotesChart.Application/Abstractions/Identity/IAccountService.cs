using DailyNotesChart.Application.DTOs.Identity;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Application.Abstractions.Identity;

public interface IAccountService
{
    Task<Result<UserInformationDto>> RegisterAsync(string userName, string email, string password);
    Task<Result> CheckIfPasswordValidByUserNameAsync(string userName, string password);
    Task<Result> CheckIfPasswordValidByEmailAsync(string email, string password);
    Task<Result<UserInformationDto>> GetUserInformationByName(string userName);
    Task<Result<UserInformationDto>> GetUserInformationByRefreshTokenAsync(string refreshToken);
    Task<Result<UserInformationDto>> GetUserInformationByEmail(string email);

    /// <returns>New token if check successfull: refresh token exists and not expired</returns>
    Task<Result<(string accessToken, string refreshToken)>> CheckRefreshTokenAsync(string refreshToken);
}