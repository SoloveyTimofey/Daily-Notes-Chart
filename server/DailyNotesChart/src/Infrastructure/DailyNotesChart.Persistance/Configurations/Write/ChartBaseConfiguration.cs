using DailyNotesChart.Domain.Models.ChartAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyNotesChart.Persistance.Configurations.Write;

internal sealed class ChartBaseConfiguration : IEntityTypeConfiguration<ChartBase>
{
    public void Configure(EntityTypeBuilder<ChartBase> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
                chartId => chartId.Id,
                value => new ChartId(value)
            );
        
        builder.Property(c => c.Date)
            .HasConversion(
                date => date.Value,
                value => ChartDate.Create(value).Value!
            );

        builder.Property(c => c.Summary)
            .HasConversion(
                summary => summary.Value,
                value => ChartSummary.Create(value).Value!
            )
            .HasMaxLength(ChartSummary.SUMMARY_MAX_LENGHT)
            .IsRequired(false);

        // Note: need to check after the mapping strategy will be resolved
        //builder.OwnsMany(c => c.Notes, nb =>
        //{
        //    // Configuration for NoteBase is here:
        //    nb.WithOwner().HasForeignKey(note => note.ChartId);
        //    nb.Property<int>("Id");
        //    nb.HasKey("Id");

        //    nb.Property(note => note.Color).HasConversion(
        //        color => color.Value,
        //        value => Color.Create(value).Value!
        //    ).HasColumnType("nvarchar(7)");

        //    nb.Property(note => note.Description).HasConversion(
        //        description => description.Value,
        //        value => NoteDescription.Create(value).Value!
        //    );
        //});

        builder.HasMany(c => c.Notes)
            .WithOne()
            .HasForeignKey(n => n.ChartId);
    }
}