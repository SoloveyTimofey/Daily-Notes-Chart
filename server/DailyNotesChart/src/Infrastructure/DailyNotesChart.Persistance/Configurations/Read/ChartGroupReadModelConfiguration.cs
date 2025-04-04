using DailyNotesChart.Application.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DailyNotesChart.Persistance.Configurations.Read;

internal sealed class ChartGroupReadModelConfiguration : IEntityTypeConfiguration<ChartGroupReadModel>
{
    private class BooleanJsonConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return bool.TryParse(reader.GetString(), out bool result) && result;
            }
            return reader.GetBoolean();
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            throw new NotSupportedException("Cannot use write operations in read models.");
        }
    }
    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        Converters = { new BooleanJsonConverter() },
    };

    public void Configure(EntityTypeBuilder<ChartGroupReadModel> builder)
    {
        builder.HasKey(cg => cg.Id);

        builder.Property(cg => cg.DefaultChartTemplate)
            .HasConversion(
                value => JsonSerializer.Serialize(value, _jsonOptions),
                value => JsonSerializer.Deserialize<DefaultChartTemplateReadModel>(value, _jsonOptions)!
            );

        builder.HasOne(cg => cg.DefaultNoteTemplate)
            .WithMany()
            .HasForeignKey(cg => cg.DefaultNoteTemplateId)
            .IsRequired(false);


        builder.HasMany(cg => cg.Charts)
            .WithOne()
            .HasForeignKey(c => c.ChartGroupId);
        builder.Navigation(cg => cg.Charts);

        builder.HasMany(cg => cg.NoteTemplates)
            .WithOne()
            .HasForeignKey(nt => nt.ChartGroupId);
        builder.Navigation(cg => cg.NoteTemplates);
    }
}