using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyNotesChart.Persistance.Configurations;

internal sealed class TimeOnlyNoteConfiguration : IEntityTypeConfiguration<TimeOnlyNote>
{
    public void Configure(EntityTypeBuilder<TimeOnlyNote> builder)
    {
        builder.HasBaseType<NoteBase>();
    }
}