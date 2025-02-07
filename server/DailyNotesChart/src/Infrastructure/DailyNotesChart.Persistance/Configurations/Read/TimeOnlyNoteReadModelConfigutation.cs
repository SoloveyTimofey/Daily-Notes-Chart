using DailyNotesChart.Application.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyNotesChart.Persistance.Configurations.Read;

internal sealed class TimeOnlyNoteReadModelConfigutation : IEntityTypeConfiguration<TimeOnlyNoteReadModel>
{
    public void Configure(EntityTypeBuilder<TimeOnlyNoteReadModel> builder)
    {
        builder.HasBaseType<NoteBaseReadModel>();
    }
}