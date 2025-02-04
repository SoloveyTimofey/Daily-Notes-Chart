namespace DailyNotesChart.WebApi.Requests.NoteTemplates;

public sealed record CreateNoteTemplateRequest(
    Guid ChartGroupId,
    string Color,
    string NoteDescription
);
