using DailyNotesChart.Domain.Models.ChartAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyNotesChart.Persistance.Configurations.Write;

internal sealed class TwoDimensionalChartConfiguration : IEntityTypeConfiguration<TwoDimentionalChart>
{
    public void Configure(EntityTypeBuilder<TwoDimentionalChart> builder)
    {
        builder.OwnsOne(c => c.YAxeValues, yAxeValuesBuilder =>
        {
            yAxeValuesBuilder.Property(yAxeVal => yAxeVal.Start).HasMaxLength(YAxeValues.MAX_VALUE);
            yAxeValuesBuilder.Property(yAxeVal => yAxeVal.End).HasMaxLength(YAxeValues.MAX_VALUE);
            yAxeValuesBuilder.Property(yAxeVal => yAxeVal.IsInteger).HasColumnType("bit");
            yAxeValuesBuilder.Property(yAxeVal => yAxeVal.IsInteger).HasColumnName("YAxevalues_IsInteger");
            yAxeValuesBuilder.Property(yAxeVal => yAxeVal.IsInteger);
        });

        builder.Property(c => c.YAxeName)
            .HasConversion(
                yAxeName => yAxeName.Value,
                value => YAxeName.Create(value).Value!
            ).HasMaxLength(YAxeName.NAME_MAX_LENGHT);

        builder.HasBaseType<ChartBase>();
    }
}