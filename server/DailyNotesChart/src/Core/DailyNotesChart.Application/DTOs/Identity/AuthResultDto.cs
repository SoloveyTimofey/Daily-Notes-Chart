namespace DailyNotesChart.Application.DTOs.Identity;

public sealed record AuthResultDto(Guid UserId, string UserName, string UserEmail, string[] Roles, string Token, string RefreshToken);