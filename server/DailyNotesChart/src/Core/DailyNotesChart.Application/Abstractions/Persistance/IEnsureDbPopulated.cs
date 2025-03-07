namespace DailyNotesChart.Application.Abstractions.Persistance;

public interface IEnsureDbPopulated
{
    Task Ensure();
}