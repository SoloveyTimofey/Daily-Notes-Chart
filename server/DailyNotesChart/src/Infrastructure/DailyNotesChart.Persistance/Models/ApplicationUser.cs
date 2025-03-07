using Microsoft.AspNetCore.Identity;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;

namespace DailyNotesChart.Persistance.Models;

internal class ApplicationUser : IdentityUser<ApplicationUserId>
{
    public ApplicationUser()
    {
        Id = new ApplicationUserId(Guid.NewGuid());
    }

    public List<ChartGroup> ChartGroups { get; set; } = new();
    public List<RefreshToken> RefreshTokens { get; set; } = new();
}