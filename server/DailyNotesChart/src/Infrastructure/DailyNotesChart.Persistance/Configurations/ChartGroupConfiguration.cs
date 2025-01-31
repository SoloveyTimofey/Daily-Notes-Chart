using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace DailyNotesChart.Persistance.Configurations;

internal sealed class ChartGroupConfiguration : IEntityTypeConfiguration<ChartGroup>
{
    public void Configure(EntityTypeBuilder<ChartGroup> builder)
    {
        builder.HasKey(cg => cg.Id);

        builder.Property(cg => cg.Id)
            .ValueGeneratedNever()
            .HasConversion(
                chartGroupId => chartGroupId.Id,
                value => new ChartGroupId(value)
            );

        builder.Property(cg => cg.Name)
            .HasConversion(chartGroupName => chartGroupName.Value, value => ChartGroupName.Create(value).Value!)
            .HasMaxLength(ChartGroupName.NAME_MAX_LENGHT);

        builder.Property(cg => cg.DefaultChartTemplate)
            .HasConversion(
                value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
                value => JsonSerializer.Deserialize<DefaultChartTemplate>(value, (JsonSerializerOptions?)null)!
            );

        //builder.OwnsOne(cg => cg.DefaultChartTemplate, ownedNavigationBuilder =>
        //{
        //    ownedNavigationBuilder.ToJson();
        //});

        builder.HasMany(cg => cg.Charts)
            .WithOne()
            .HasForeignKey(chart => chart.ChartGroupId);

        builder.HasMany(cg => cg.NoteTemplates)
            .WithOne()
            .HasForeignKey(noteTemplate => noteTemplate.ChartGroupId);

        builder.HasOne(cg => cg.DefaultNoteTemplate)
            .WithMany()
            .HasForeignKey(cg => cg.DefaultNoteTemplateId)
            .IsRequired(false);
    }
}