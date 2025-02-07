using DailyNotesChart.Application.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyNotesChart.Persistance.Configurations.Read;

internal sealed class TwoDimensionalNoteReadModelConfiguration : IEntityTypeConfiguration<TwoDimensionalNoteReadModel>
{
    public void Configure(EntityTypeBuilder<TwoDimensionalNoteReadModel> builder)
    {
        builder.HasBaseType<NoteBaseReadModel>();
    }
}