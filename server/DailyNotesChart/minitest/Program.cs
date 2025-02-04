using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;

var id = new ChartId(Guid.NewGuid());

Console.WriteLine(id.Id.ToString());