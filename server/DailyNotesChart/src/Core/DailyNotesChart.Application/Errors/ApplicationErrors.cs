using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Application.Errors;

public static class ApplicationErrors
{
    public static class Account
    {
        public static readonly Error InvalidUserNameOrPassword = new Error(
            "Account.InvalidUserNameOrPassword",
            "Invalid user name or password."
        );

        public static readonly Error InvalidEmailOrPassword = new Error(
            "Account.InvalidEmailOrPassword",
            "Invalid email or password."
        );

        public static readonly Error RefreshTokenExpiredOrDoesNotExist = new Error(
            "Account.RefreshTokenExpiredOrDoesNotExist",
            "Provided refresh token expired or does not exist."
        );
    }
}