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

        builder.HasMany(c => c.Notes)
           .WithOne()
           .HasForeignKey(n => n.ChartId);

        builder.Property(c => c.Summary).IsRequired(false);

        builder.HasDiscriminator<string>("Discriminator")
            .HasValue<TwoDimensionalChartReadModel>("TwoDimensionalChart")
            .HasValue<TimeOnlyChartReadModel>("TimeOnlyChart");
    }
}