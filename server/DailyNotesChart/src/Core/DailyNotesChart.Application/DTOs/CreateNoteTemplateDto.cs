namespace DailyNotesChart.Application.DTOs;

public sealed record CreateNoteTemplateDto(
    string Color, 
    string NoteDescription
);