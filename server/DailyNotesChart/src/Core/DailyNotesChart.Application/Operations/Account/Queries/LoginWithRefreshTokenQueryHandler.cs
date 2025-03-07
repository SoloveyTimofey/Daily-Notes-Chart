using DailyNotesChart.Application.Abstractions.Identity;
using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.DTOs.Identity;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Application.Operations.Account.Queries;

internal sealed class LoginWithRefreshTokenQueryHandler : HandlerBase<TokenDto>, IQueryHandler<LoginWithRefreshTokenQuery, TokenDto>
{
    private readonly IAccountService _accountService;

    public LoginWithRefreshTokenQueryHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<Result<TokenDto>> Handle(LoginWithRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var checkRefreshTokenResult = await _accountService.CheckRefreshTokenAsync(request.RefreshToken);
        if (checkRefreshTokenResult.IsFailure)
            return Failure(checkRefreshTokenResult);

        var tokenValue = checkRefreshTokenResult.Value!;

        return Result.Success(
            new TokenDto(tokenValue.accessToken, tokenValue.refreshToken)
        );
    }
}