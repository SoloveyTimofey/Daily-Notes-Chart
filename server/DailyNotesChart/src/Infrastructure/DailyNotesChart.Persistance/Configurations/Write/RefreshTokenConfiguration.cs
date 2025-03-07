using DailyNotesChart.Persistance.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyNotesChart.Persistance.Configurations.Write;

internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Token).HasMaxLength(200);

        builder.HasIndex(r => r.Token).IsUnique();

        builder.HasOne(r => r.ApplicationUser)
             .WithMany(u => u.RefreshTokens) // Добавил явную привязку
             .HasForeignKey(r => r.ApplicationUserId)
             .OnDelete(DeleteBehavior.Cascade);
    }
}