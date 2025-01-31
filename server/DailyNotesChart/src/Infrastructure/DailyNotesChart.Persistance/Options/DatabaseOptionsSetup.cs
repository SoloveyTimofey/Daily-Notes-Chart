using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DailyNotesChart.Persistance.Options;

internal class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
{
    private readonly IConfiguration _configuration;
    private const string CONFIGURATION_SECTION_NAME = "DatabaseOptions";
    public DatabaseOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(DatabaseOptions options)
    {
        var connectionString = _configuration.GetConnectionString("DailyNotesChartConnection");

        options.ConnectionString = connectionString ?? throw new ArgumentNullException(connectionString);

        _configuration.GetSection(CONFIGURATION_SECTION_NAME);
    }
}