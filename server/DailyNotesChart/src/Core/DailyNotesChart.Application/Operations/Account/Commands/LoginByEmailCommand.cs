  using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.DTOs.Identity;

namespace DailyNotesChart.Application.Operations.Account.Commands;

public sealed record LoginByEmailCommand(
    string Email,
    string Password
) : ICommand<AuthResultDto>;