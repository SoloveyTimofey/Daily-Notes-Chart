using DailyNotesChart.Application.Abstractions.Identity;
using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.DTOs.Identity;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Application.Operations.Account.Queries;

internal class LoginByUserNameQueryHandler : HandlerBase<TokenDto>, IQueryHandler<LoginByUserNameQuery, TokenDto>
{
    private readonly ITokenProvider _tokenProvider;
    private readonly IAccountService _accountService;

    public LoginByUserNameQueryHandler(ITokenProvider tokenProvider, IAccountService accountService)
    {
        _tokenProvider = tokenProvider;
        _accountService = accountService;
    }

    public async Task<Result<TokenDto>> Handle(LoginByUserNameQuery request, CancellationToken cancellationToken)
    {
        var getIdResult = await _accountService.GetIdByUserName(request.UserName);
        if (getIdResult.IsFailure)
            return Failure(getIdResult);

        ApplicationUserId userId = getIdResult.Value!;

        var isPasswordValidResult = await _accountService.CheckIfPasswordValidByUserNameAsync(request.UserName, request.Password);
        if (isPasswordValidResult.IsFailure)
            return Result.Failure<TokenDto>([.. isPasswordValidResult.Errors]);

        var tokenResult = await _tokenProvider.GenerateTokenForUserByUserNameAsync(request.UserName);

        if (tokenResult.IsFailure)
            return Failure(tokenResult);

        var refreshToken = await _tokenProvider.GenerateRefreshTokenForUserAsync(userId);

        return Result.Success(
            new TokenDto(tokenResult.Value!, refreshToken)
        );
    }
}