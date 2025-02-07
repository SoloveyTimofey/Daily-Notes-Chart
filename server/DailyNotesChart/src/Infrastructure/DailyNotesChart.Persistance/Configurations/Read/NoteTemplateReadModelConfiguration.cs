using DailyNotesChart.Application.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyNotesChart.Persistance.Configurations.Read;

internal sealed class NoteTemplateReadModelConfiguration : IEntityTypeConfiguration<NoteTemplateReadModel>
{
    public void Configure(EntityTypeBuilder<NoteTemplateReadModel> builder)
    {
        builder.HasKey(nt => nt.Id);   
    }
}