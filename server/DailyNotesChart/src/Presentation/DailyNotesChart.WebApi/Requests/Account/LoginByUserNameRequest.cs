namespace DailyNotesChart.WebApi.Requests.Account;

public sealed record LoginByUserNameRequest(
    string UserName,
    string Password
);