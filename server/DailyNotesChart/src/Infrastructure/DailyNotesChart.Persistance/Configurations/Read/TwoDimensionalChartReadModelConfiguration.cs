using DailyNotesChart.Application.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyNotesChart.Persistance.Configurations.Read;

internal sealed class TwoDimensionalChartReadModelConfiguration : IEntityTypeConfiguration<TwoDimensionalChartReadModel>
{
    public void Configure(EntityTypeBuilder<TwoDimensionalChartReadModel> builder)
    {
        builder.HasBaseType<ChartBaseReadModel>();

        builder.OwnsOne(c => c.YAxeValues);
    }
}
