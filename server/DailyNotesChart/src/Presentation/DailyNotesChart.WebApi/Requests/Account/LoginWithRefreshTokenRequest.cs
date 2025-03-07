namespace DailyNotesChart.WebApi.Requests.Account;

public sealed record LoginWithRefreshTokenRequest(
    string RefreshToken    
);