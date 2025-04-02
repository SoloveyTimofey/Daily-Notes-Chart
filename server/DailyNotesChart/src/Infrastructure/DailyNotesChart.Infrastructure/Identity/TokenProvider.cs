using DailyNotesChart.Application.Abstractions.Identity;
using DailyNotesChart.Application.Abstractions.Persistance;
using DailyNotesChart.Application.Exceptions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Shared.ResultPattern;
using DailyNotesChart.Persistance.InternalAbstractions;
using DailyNotesChart.Persistance.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DailyNotesChart.Infrastructure.Identity;

internal sealed class TokenProvider : ITokenProvider
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IConfiguration _configuration;

    public TokenProvider(UserManager<ApplicationUser> userManager, IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository)
    {
        _userManager = userManager;
        _configuration = configuration;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<Result<string>> GenerateAccessTokenForUserByEmailAsync(string userEmail)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(userEmail);
        if (user is null)
            throw new EntityWithSpecifiedIdDoesNotExistException(nameof(ApplicationUser), userEmail);

        string token = await GenerateToken(user);

        return Result.Success(
            token
        );
    }

    public async Task<Result<string>> GenerateAccessTokenForUserByUserNameAsync(string userName)
    {
        ApplicationUser? user = await _userManager.FindByNameAsync(userName);
        if (user is null)
            throw new EntityWithSpecifiedIdDoesNotExistException(nameof(ApplicationUser), userName);

        string token = await GenerateToken(user);

        return Result.Success(
            token
        );
    }

    public async Task<string> GenerateRefreshTokenForUserAsync(ApplicationUserId userId)
    {
        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        var minutesAfterRefreshTokenExpires = int.Parse(_configuration["JwtConfiguration:MinutesAfterRefreshTokenExpires"]!);
        var refreshTokenDbModel = new RefreshToken
        {
            Id = Guid.NewGuid(),
            ApplicationUserId = userId,
            Token = refreshToken,
            CreatedOnUtc = DateTime.UtcNow,
            ExpiresOnUtc = DateTime.UtcNow.AddMinutes(minutesAfterRefreshTokenExpires)
        };

        _refreshTokenRepository.Add(refreshTokenDbModel);
        await _refreshTokenRepository.RemovePreviousRefreshTokensForSpecifiedApplicationUserAsync(userId); // Remoe previous refresh tokens

        return refreshToken;
    }

    private async Task<string> GenerateToken(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var secret = _configuration["JwtConfiguration:Secret"];
        var issuer = _configuration["JwtConfiguration:ValidIssuer"];
        var audience = _configuration["JwtConfiguration:ValidAudiences"];
        var minutesAfterTokenExpires = int.Parse(_configuration["JwtConfiguration:MinutesAfterTokenExpires"]!);
        if (secret is null || issuer is null || audience is null)
            throw new ApplicationException("Jwt info is not set in the configuration.");

        var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName!)
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(minutesAfterTokenExpires),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        var token = tokenHandler.WriteToken(securityToken);

        return token;
    }
}