using DailyNotesChart.Application.Abstractions.Identity;
using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.DTOs.Identity;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Application.Operations.Account.Commands;

internal sealed class RegisterCommandHandler : HandlerBase<TokenDto>, ICommandHandler<RegisterCommand, TokenDto>
{
    private readonly IAccountService _accountService;
    private readonly ITokenProvider _tokenProvider;

    public RegisterCommandHandler(IAccountService accountService, ITokenProvider tokenProvider)
    {
        _accountService = accountService;
        _tokenProvider = tokenProvider;
    }

    public async Task<Result<TokenDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var registerResult = await _accountService.RegisterAsync(request.UserName, request.Email, request.Password);
        if (registerResult.IsFailure)
            return Failure(registerResult);

        var tokenResult = await _tokenProvider.GenerateTokenForUserByEmailAsync(request.Email);
        if (tokenResult.IsFailure)
            return Failure(tokenResult);

        var refreshToken = await _tokenProvider.GenerateRefreshTokenForUserAsync(registerResult.Value!);

        return Result.Success(
            new TokenDto(tokenResult.Value!, refreshToken)
        );
    }
}