using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyNotesChart.Persistance.Configurations.Write;

internal sealed class NoteTemplateConfiguration : IEntityTypeConfiguration<NoteTemplate>
{
    public void Configure(EntityTypeBuilder<NoteTemplate> builder)
    {
        builder.HasKey(nt => nt.Id);

        builder.Property(nt => nt.Id)
            .ValueGeneratedNever()
            .HasConversion(noteTemplateId => noteTemplateId.Id, value => new NoteTemplateId(value));

        builder.Property(nt => nt.Color)
            .HasConversion(color => color.Value, value => Color.Create(value).Value!);

        builder.Property(nt => nt.Description)
            .HasConversion(description => description.Value, value => NoteDescription.Create(value).Value!)
            .HasMaxLength(NoteDescription.DESCRIPTION_MAX_LENGHT);
    }
}