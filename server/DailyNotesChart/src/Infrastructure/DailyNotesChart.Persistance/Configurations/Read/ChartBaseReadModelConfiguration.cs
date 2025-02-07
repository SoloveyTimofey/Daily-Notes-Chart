using DailyNotesChart.Application.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyNotesChart.Persistance.Configurations.Read;

internal sealed class ChartBaseReadModelConfiguration : IEntityTypeConfiguration<ChartBaseReadModel>
{
    public void Configure(EntityTypeBuilder<ChartBaseReadModel> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Navigation(c => c.Notes);

        builder.UseTphMappingStrategy();

        builder.HasOne(c => c.ChartGroup)
            .WithMany()
            .HasForeignKey(c => c.ChartGroupId);

        builder.HasMany(c => c.Notes)
           .WithOne()
           .HasForeignKey(n => n.ChartId);

        builder.Property(c => c.Summary).IsRequired(false);
    }
}