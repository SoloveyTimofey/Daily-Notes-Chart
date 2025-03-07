using DailyNotesChart.Application.Abstractions.Identity;
using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.DTOs.Identity;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Application.Operations.Account.Commands;

internal sealed class LoginByEmailQueryHandler : HandlerBase<TokenDto>, ICommandHandler<LoginByEmailCommand, TokenDto>
{
    private readonly ITokenProvider _tokenProvider;
    private readonly IAccountService _accountService;

    public LoginByEmailQueryHandler(ITokenProvider tokenProvider, IAccountService accountService)
    {
        _tokenProvider = tokenProvider;
        _accountService = accountService;
    }

    public async Task<Result<TokenDto>> Handle(LoginByEmailCommand request, CancellationToken cancellationToken)
    {
        var getIdResult = await _accountService.GetIdByUserEmail(request.Email);
        if (getIdResult.IsFailure)
            return Failure(getIdResult);

        ApplicationUserId userId = getIdResult.Value!;

        var isPasswordValidResult = await _accountService.CheckIfPasswordValidByEmailAsync(request.Email, request.Password);
        if (isPasswordValidResult.IsFailure)
            return Result.Failure<TokenDto>([.. isPasswordValidResult.Errors]);

        var tokenResult = await _tokenProvider.GenerateTokenForUserByEmailAsync(request.Email);

        if (tokenResult.IsFailure)
            return Failure(tokenResult);

        var refreshToken = await _tokenProvider.GenerateRefreshTokenForUserAsync(userId);

        return Result.Success(
            new TokenDto(tokenResult.Value!, refreshToken)
        );
    }
}