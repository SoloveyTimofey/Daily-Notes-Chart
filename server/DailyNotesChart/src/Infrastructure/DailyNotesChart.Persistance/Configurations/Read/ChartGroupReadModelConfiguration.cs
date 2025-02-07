using DailyNotesChart.Application.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace DailyNotesChart.Persistance.Configurations.Read;

internal sealed class ChartGroupReadModelConfiguration : IEntityTypeConfiguration<ChartGroupReadModel>
{
    public void Configure(EntityTypeBuilder<ChartGroupReadModel> builder)
    {
        builder.HasKey(cg => cg.Id);

        builder.HasOne(cg => cg.DefaultNoteTemplate)
            .WithMany()
            .HasForeignKey(cg => cg.DefaultNoteTemplateId)
            .IsRequired(false);

        builder.Property(cg => cg.DefaultChartTemplate)
            .HasConversion(
                value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
                value => JsonSerializer.Deserialize<DefaultChartTemplateReadModel>(value, (JsonSerializerOptions?)null)!
            );

        builder.Navigation(cg => cg.NoteTemplates);
        builder.Navigation(cg => cg.Charts);
    }
}