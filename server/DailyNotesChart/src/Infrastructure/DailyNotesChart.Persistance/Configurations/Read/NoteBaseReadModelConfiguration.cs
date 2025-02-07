using DailyNotesChart.Application.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyNotesChart.Persistance.Configurations.Read;

internal class NoteBaseReadModelConfiguration : IEntityTypeConfiguration<NoteBaseReadModel>
{
    public void Configure(EntityTypeBuilder<NoteBaseReadModel> builder)
    {
        builder.Property<int>("Id");
        builder.HasKey("Id");

        builder.UseTphMappingStrategy();
    }
}