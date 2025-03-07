namespace DailyNotesChart.Application.Shared;

public static class Roles
{
    public const string Administrator = "Administrator";
    public const string User = "User";

    public static IEnumerable<string> GetAllRoles()
    {
        yield return Administrator;
        yield return User;
    }
}