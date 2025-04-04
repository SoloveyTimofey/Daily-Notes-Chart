using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyNotesChart.Persistance.Configurations.Write;

internal sealed class TwoDimensionalNoteConfiguration : IEntityTypeConfiguration<TwoDimensionalNote>
{
    public void Configure(EntityTypeBuilder<TwoDimensionalNote> builder)
    {
        builder.HasBaseType<NoteBase>();
    }
}