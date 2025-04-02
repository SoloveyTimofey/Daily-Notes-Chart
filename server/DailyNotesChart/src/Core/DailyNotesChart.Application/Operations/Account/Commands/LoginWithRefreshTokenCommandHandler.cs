using DailyNotesChart.Application.Abstractions.Identity;
using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.DTOs.Identity;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Application.Operations.Account.Commands;

internal sealed class LoginWithRefreshTokenCommandHandler : HandlerBase<AuthResultDto>, ICommandHandler<LoginWithRefreshTokenCommand, AuthResultDto>
{
    private readonly IAccountService _accountService;

    public LoginWithRefreshTokenCommandHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<Result<AuthResultDto>> Handle(LoginWithRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var checkRefreshTokenResult = await _accountService.CheckRefreshTokenAsync(request.RefreshToken);
        if (checkRefreshTokenResult.IsFailure)
            return Failure(checkRefreshTokenResult);

        var tokenValue = checkRefreshTokenResult.Value!;

        var userInfoResult = await _accountService.GetUserInformationByRefreshTokenAsync(tokenValue.refreshToken);
        if(userInfoResult.IsFailure)
            return Failure(userInfoResult);

        var userInfoValue = userInfoResult.Value!;

        return Result.Success(
            new AuthResultDto(userInfoValue.UserId.Id, userInfoValue.UserName, userInfoValue.UserEmail, userInfoValue.Roles, tokenValue.accessToken, tokenValue.refreshToken)
        );
    }
}