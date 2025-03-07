using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;

namespace DailyNotesChart.Persistance.Models;

internal class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; } = null!;
    public DateTime CreatedOnUtc { get; set; }
    public DateTime ExpiresOnUtc { get; set; }

    public ApplicationUserId ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
}
