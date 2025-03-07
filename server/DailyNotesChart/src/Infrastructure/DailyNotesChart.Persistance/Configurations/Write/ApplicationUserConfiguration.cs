using DailyNotesChart.Persistance.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;

namespace DailyNotesChart.Persistance.Configurations.Write;

internal sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.Id)
            .HasConversion(applicationUserId => applicationUserId.Id, value => new ApplicationUserId(value));

        builder.HasMany(u => u.RefreshTokens)
            .WithOne(r => r.ApplicationUser)
            .HasForeignKey(r => r.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}