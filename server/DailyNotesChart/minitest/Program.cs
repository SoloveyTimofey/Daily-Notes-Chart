using DailyNotesChart.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

var readOptionsBuilder = new DbContextOptionsBuilder<DailyNotesChartReadDbContext>();
readOptionsBuilder.UseSqlServer("Server=.;Database=DailyNotesChart;User Id=sa;Password=root;MultipleActiveResultSets=True;TrustServerCertificate=True;");

using var readContext = new DailyNotesChartReadDbContext(readOptionsBuilder.Options);

var writeOptionsBuilder = new DbContextOptionsBuilder<DailyNotesChartWriteDbContext>();
writeOptionsBuilder.UseSqlServer("Server=.;Database=DailyNotesChart;User Id=sa;Password=root;MultipleActiveResultSets=True;TrustServerCertificate=True;");

using var writeContext = new DailyNotesChartWriteDbContext(writeOptionsBuilder.Options);

var chartGroupsFromReadContext = readContext.Charts;
var res = chartGroupsFromReadContext.ToList();
var chartGroupsFromWriteContext = writeContext.ChartGroups;

Console.ReadLine();