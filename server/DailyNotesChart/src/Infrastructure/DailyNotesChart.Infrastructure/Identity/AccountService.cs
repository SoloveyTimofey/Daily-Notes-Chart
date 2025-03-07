using DailyNotesChart.Application.Abstractions.Identity;
using DailyNotesChart.Application.Abstractions.Persistance;
using DailyNotesChart.Application.Errors;
using DailyNotesChart.Application.Shared;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Shared.ResultPattern;
using DailyNotesChart.Infrastructure.Extensions;
using DailyNotesChart.Persistance.InternalAbstractions;
using DailyNotesChart.Persistance.Models;
using Microsoft.AspNetCore.Identity;

namespace DailyNotesChart.Infrastructure.Identity;

internal sealed class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<ApplicationUserId>> _roleManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public AccountService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<ApplicationUserId>> roleManager, ITokenProvider tokenProvider, IRefreshTokenRepository refreshTokenRepository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenProvider = tokenProvider;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<Result> CheckIfPasswordValidByEmailAsync(string email, string password)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return Result.Failure(ApplicationErrors.Account.InvalidEmailOrPassword);

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        return Result.Success(
            isPasswordValid
        );
    }

    public async Task<Result> CheckIfPasswordValidByUserNameAsync(string userName, string password)
    {
        ApplicationUser? user = await _userManager.FindByNameAsync(userName);
        if (user is null)
            return Result.Failure(ApplicationErrors.Account.InvalidEmailOrPassword);

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        return Result.Success(
            isPasswordValid
        );
    }

    public async Task<Result<(string accessToken, string refreshToken)>> CheckRefreshTokenAsync(string refreshToken)
    {
        RefreshToken? refreshTokenDbModel = await _refreshTokenRepository.GetFirstOrDefaultByRefreshTokenValueAsync(refreshToken);

        if (refreshTokenDbModel is null || refreshTokenDbModel.ExpiresOnUtc < DateTime.UtcNow)
        {
            return Result.Failure<(string accessToken, string refreshToken)>(ApplicationErrors.Account.RefreshTokenExpiredOrDoesNotExist);
        }

        // Remove previous refresh tokens
        //_context.RemoveRange(_context.RefreshTokens.Where(t => t.ApplicationUserId == refreshTokenDbModel.ApplicationUserId));
        //await _context.SaveChangesAsync();

        string newRefreshToken = await _tokenProvider.GenerateRefreshTokenForUserAsync(refreshTokenDbModel.ApplicationUserId);
        var generateAccessTokenResult = await _tokenProvider.GenerateTokenForUserByEmailAsync(refreshTokenDbModel.ApplicationUser.Email!);
        var newAccessToken = generateAccessTokenResult.Value!;

        return Result.Success(
            (accessToken: newAccessToken, refreshToken: newRefreshToken)
        );
    }

    public async Task<Result<ApplicationUserId>> GetIdByUserEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user != null 
            ? Result.Success(user.Id) 
            : Result.Failure<ApplicationUserId>(ApplicationErrors.Account.InvalidEmailOrPassword);
    }

    public async Task<Result<ApplicationUserId>> GetIdByUserName(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        return user != null
            ? Result.Success(user.Id)
            : Result.Failure<ApplicationUserId>(ApplicationErrors.Account.InvalidUserNameOrPassword);
    }

    public async Task<Result<ApplicationUserId>> RegisterAsync(string userName, string email, string password)
    {
        var applicationUser = new ApplicationUser
        {
            Id = new ApplicationUserId(Guid.NewGuid()),
            UserName = userName,
            Email = email
        };

        IdentityResult createUserResult = await _userManager.CreateAsync(applicationUser, password);
        if (createUserResult.Succeeded is false)
            return createUserResult.ToFailureResult<ApplicationUserId>();

        IdentityResult addToRoleResult = await _userManager.AddToRoleAsync(applicationUser, Roles.User);
        if (addToRoleResult.Succeeded is false)
            return addToRoleResult.ToFailureResult<ApplicationUserId>();

        return Result.Success(
            applicationUser.Id
        );
    }
}