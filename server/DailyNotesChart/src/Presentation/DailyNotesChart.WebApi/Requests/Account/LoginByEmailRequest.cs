namespace DailyNotesChart.WebApi.Requests.Account;

public sealed record LoginByEmailRequest(
  string Email,
  string Password
);