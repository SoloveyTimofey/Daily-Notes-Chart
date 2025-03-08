using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Persistance.Contexts;
using DailyNotesChart.Persistance.InternalAbstractions;
using DailyNotesChart.Persistance.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyNotesChart.Persistance.Repositories;

internal sealed class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly DailyNotesChartWriteDbContext _context;

    public RefreshTokenRepository(DailyNotesChartWriteDbContext context)
    {
        _context = context;
    }

    public void Add(RefreshToken refreshToken)
    {
        _context.Add(refreshToken);
    }

    public async Task<RefreshToken?> GetFirstOrDefaultByRefreshTokenValueAsync(string refreshTokenValue)
        => await _context.RefreshTokens
            .Include(r => r.ApplicationUser)
            .FirstOrDefaultAsync(r => r.Token == refreshTokenValue);

    public async Task RemovePreviousRefreshTokensForSpecifiedApplicationUserAsync(ApplicationUserId userId)
    {
        var tokensToRemove = await _context.RefreshTokens
            .Where(token => token.ApplicationUserId == userId)
            .OrderBy(token => token.CreatedOnUtc)
            .ToListAsync();

        if (tokensToRemove.Any())
        {
            _context.RefreshTokens.RemoveRange(tokensToRemove);
        }
    }
}