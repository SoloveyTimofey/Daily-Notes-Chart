using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.DTOs.Identity;

namespace DailyNotesChart.Application.Operations.Account.Queries;

public sealed record LoginByEmailQuery(
    string Email,
    string Password
) : IQuery<TokenDto>;