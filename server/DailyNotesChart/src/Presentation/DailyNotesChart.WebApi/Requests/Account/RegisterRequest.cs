using System.ComponentModel.DataAnnotations;

namespace DailyNotesChart.WebApi.Requests.Account;

public sealed record RegisterRequest(
    [Required(ErrorMessage = "User name is required.")] string UserName,
    [Required(ErrorMessage = "EmailIsRequired.")] string Email,
    [Required(ErrorMessage = "Password is required.")] string Password
);