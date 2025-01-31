using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyNotesChart.Persistance.Configurations;

internal sealed class NoteBaseConfiguration : IEntityTypeConfiguration<NoteBase>
{
    public void Configure(EntityTypeBuilder<NoteBase> builder)
    {
        builder.Property<int>("Id");
        builder.HasKey("Id");

        builder.Property(n => n.Color)
            .HasConversion(color => color.Value, value => Color.Create(value).Value!)
            .HasColumnType("nvarchar(7)");

        builder.Property(n => n.Description)
            .HasConversion(description => description.Value, value => NoteDescription.Create(value).Value!);
    }
}