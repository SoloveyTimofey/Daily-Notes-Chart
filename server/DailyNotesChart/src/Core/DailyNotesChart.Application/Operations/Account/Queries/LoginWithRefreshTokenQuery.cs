using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.DTOs.Identity;

namespace DailyNotesChart.Application.Operations.Account.Queries;

public sealed record LoginWithRefreshTokenQuery(
    string RefreshToken  
) : IQuery<TokenDto>;