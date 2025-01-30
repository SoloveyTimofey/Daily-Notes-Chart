﻿using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;

namespace DailyNotesChart.Domain.UnitTests.Models.ChartGroupAggregate.ChartCluster.ValueObjects;

[TestFixture]
public sealed class ChartDateTests
{
    [Test]
    public void Create_PassTooEarlyDate_ReturnsValidError()
    {
        // Assign
        var tooEarlyDate = ChartDate.MIN_DATE.AddDays(-1);

        // Act
        var result = ChartDate.Create(tooEarlyDate);

        // Assert
        Assert.That(result.Error, Is.EqualTo(DomainErrors.Chart.InvaildChartDate));
    }

    [Test]
    public void Create_PassTooLateDate_ReturnsValidError()
    {
        // Assign
        var tooLateDate = ChartDate.MAX_DATE.AddDays(1);

        // Act
        var result = ChartDate.Create(tooLateDate);

        // Assert
        Assert.That(result.Error, Is.EqualTo(DomainErrors.Chart.InvaildChartDate));
    }
}