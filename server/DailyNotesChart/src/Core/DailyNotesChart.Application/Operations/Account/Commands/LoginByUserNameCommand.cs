using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.DTOs.Identity;

namespace DailyNotesChart.Application.Operations.Account.Commands;

public sealed record LoginByUserNameCommand(
    string UserName,
    string Password
) : ICommand<TokenDto>;