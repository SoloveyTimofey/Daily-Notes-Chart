namespace DailyNotesChart.Application.DTOs.ChartGroups;

public sealed record CreateNoteTemplateDto(
    string Color,
    string NoteDescription
);