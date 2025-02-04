namespace DailyNotesChart.WebApi.Requests.ChartGroups;

public sealed record SetDefaultNoteTemplateRequest(
    Guid NoteTemplateId,
    Guid ChartGroupId
);