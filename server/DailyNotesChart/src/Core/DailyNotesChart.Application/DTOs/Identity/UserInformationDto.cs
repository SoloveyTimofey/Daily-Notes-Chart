using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;

namespace DailyNotesChart.Application.DTOs.Identity;

public sealed record UserInformationDto(ApplicationUserId UserId, string UserName, string UserEmail, string[] Roles);