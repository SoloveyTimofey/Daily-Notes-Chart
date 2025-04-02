using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Persistance.Models;

namespace DailyNotesChart.Persistance.InternalAbstractions;

internal interface IRefreshTokenRepository
{
    Task RemovePreviousRefreshTokensForSpecifiedApplicationUserAsync(ApplicationUserId userId);
    Task<ApplicationUser> GetRefreshTokenOwner(string refreshToken);
    void Add(RefreshToken refreshToken);
    Task<RefreshToken?> GetFirstOrDefaultByRefreshTokenValueAsync(string refreshTokenValue);
}