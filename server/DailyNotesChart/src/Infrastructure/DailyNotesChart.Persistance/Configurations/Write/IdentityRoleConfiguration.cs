using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyNotesChart.Persistance.Configurations.Write;

internal class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole<ApplicationUserId>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<ApplicationUserId>> builder)
    {
        builder.Property(r => r.Id)
            .HasConversion(applicationUserId => applicationUserId.Id, value => new ApplicationUserId(value));
    }
}