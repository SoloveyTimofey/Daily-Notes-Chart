using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyNotesChart.Persistance.Configurations;

internal sealed class TwoDimensionalNoteConfiguration : IEntityTypeConfiguration<TwoDimentionalNote>
{
    public void Configure(EntityTypeBuilder<TwoDimentionalNote> builder)
    {
        builder.HasBaseType<NoteBase>();
    }
}