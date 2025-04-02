using DailyNotesChart.Application.Abstractions.Identity;
using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.DTOs.Identity;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Application.Operations.Account.Commands;

internal class LoginByUserNameCommandHandler : HandlerBase<AuthResultDto>, ICommandHandler<LoginByUserNameCommand, AuthResultDto>
{
    private readonly ITokenProvider _tokenProvider;
    private readonly IAccountService _accountService;

    public LoginByUserNameCommandHandler(ITokenProvider tokenProvider, IAccountService accountService)
    {
        _tokenProvider = tokenProvider;
        _accountService = accountService;
    }

    public async Task<Result<AuthResultDto>> Handle(LoginByUserNameCommand request, CancellationToken cancellationToken)
    {
        var userInfo = await _accountService.GetUserInformationByName(request.UserName);
        if (userInfo.IsFailure)
            return Failure(userInfo);

        var userInfoVal = userInfo.Value!;

        var isPasswordValidResult = await _accountService.CheckIfPasswordValidByUserNameAsync(request.UserName, request.Password);
        if (isPasswordValidResult.IsFailure)
            return Result.Failure<AuthResultDto>([.. isPasswordValidResult.Errors]);

        var tokenResult = await _tokenProvider.GenerateAccessTokenForUserByUserNameAsync(request.UserName);

        if (tokenResult.IsFailure)
            return Failure(tokenResult);

        var refreshToken = await _tokenProvider.GenerateRefreshTokenForUserAsync(userInfoVal.UserId);

        return Result.Success(
            new AuthResultDto(userInfoVal.UserId.Id, userInfoVal.UserName, userInfoVal.UserEmail, userInfoVal.Roles, tokenResult.Value!, refreshToken)
        );
    }
}