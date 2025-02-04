namespace DailyNotesChart.Application.Exceptions;

public class ThisShouldNotHaveHappenedException : ApplicationException
{
    public ThisShouldNotHaveHappenedException(string message) : base(message) { }
}