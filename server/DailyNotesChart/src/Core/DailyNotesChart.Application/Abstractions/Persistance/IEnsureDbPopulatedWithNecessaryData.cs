namespace DailyNotesChart.Application.Abstractions.Persistance;

public interface IEnsureDbPopulatedWithNecessaryData
{
    Task Ensure();
}