using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DailyNotesChart.Persistance.Utils;

internal class DefaultChartTemplateConverter : JsonConverter<DefaultChartTemplate>
{
    public override DefaultChartTemplate? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var json = JsonDocument.ParseValue(ref reader);
        var template = new DefaultChartTemplate
        (
            YAxeName: YAxeName.Create(json.RootElement.GetProperty("YAxeName").GetString() ?? string.Empty).Value!,
            YAxeValues: YAxeValues.Create(
                start: double.Parse(json.RootElement.GetProperty("Start").GetString() ?? "0"),
                end: double.Parse(json.RootElement.GetProperty("End").GetString() ?? "0"),
                isInteger: bool.Parse(json.RootElement.GetProperty("IsInteger").GetString() ?? "false")
            ).Value!
        );
        return template;
    }

    public override void Write(Utf8JsonWriter writer, DefaultChartTemplate value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("YAxeName", value.YAxeName.Value);
        writer.WriteString("Start", value.YAxeValues.Start.ToString());
        writer.WriteString("End", value.YAxeValues.End.ToString());
        writer.WriteString("IsInteger", value.YAxeValues.IsInteger.ToString().ToLower());
        writer.WriteEndObject();
    }
}