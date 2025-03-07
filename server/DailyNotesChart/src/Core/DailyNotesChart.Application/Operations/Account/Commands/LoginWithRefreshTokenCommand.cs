using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.DTOs.Identity;

namespace DailyNotesChart.Application.Operations.Account.Commands;

public sealed record LoginWithRefreshTokenCommand(
    string RefreshToken
) : ICommand<TokenDto>;