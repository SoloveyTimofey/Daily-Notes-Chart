using DailyNotesChart.Application.Abstractions.Identity;
using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.DTOs.Identity;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Application.Operations.Account.Commands;

internal sealed class RegisterCommandHandler : HandlerBase<AuthResultDto>, ICommandHandler<RegisterCommand, AuthResultDto>
{
    private readonly IAccountService _accountService;
    private readonly ITokenProvider _tokenProvider;

    public RegisterCommandHandler(IAccountService accountService, ITokenProvider tokenProvider)
    {
        _accountService = accountService;
        _tokenProvider = tokenProvider;
    }

    public async Task<Result<AuthResultDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var registerResult = await _accountService.RegisterAsync(request.UserName, request.Email, request.Password);
        if (registerResult.IsFailure)
            return Failure(registerResult);

        var tokenResult = await _tokenProvider.GenerateAccessTokenForUserByEmailAsync(request.Email);
        if (tokenResult.IsFailure)
            return Failure(tokenResult);

        var userInfoValue = registerResult.Value!;

        var refreshToken = await _tokenProvider.GenerateRefreshTokenForUserAsync(userInfoValue.UserId);

        return Result.Success(
            new AuthResultDto(userInfoValue.UserId.Id, userInfoValue.UserName, userInfoValue.UserEmail, userInfoValue.Roles, tokenResult.Value!, refreshToken)
        );
    }
}