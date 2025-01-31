namespace DailyNotesChart.Persistance.Options;

internal class DatabaseOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public int MaxRetryCount { get; set; }
    public int CommandTimeout { get; set; }
    public bool EnableDetailedError { get; set; }
    public bool EnableSensitivveDataLogging { get; set; }
}