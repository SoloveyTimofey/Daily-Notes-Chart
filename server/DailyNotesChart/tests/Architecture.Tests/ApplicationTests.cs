using DailyNotesChart.Application;
using DailyNotesChart.Application.Abstractions.MediatrSpecific;

namespace Architecture.Tests;

[TestFixture]
public class ApplicationTests
{
    [Test]
    public void Handlers_ShouldNot_BePublic()
    {
        // Act
        var testResult = Types.InAssembly(typeof(ApplicationServicesRegistration).Assembly)
            .That()
            .HaveNameEndingWith("Handler")
            .ShouldNot()
            .BePublic()
            .GetResult();

        // Assert
        Assert.That(testResult.IsSuccessful);
    }

    [Test]
    public void QueriesAndCommands_Should_BePublic()
    {
        // Act
        var testResult = Types.InAssembly(typeof(ApplicationServicesRegistration).Assembly)
            .That()
            .ImplementInterface(typeof(ICommand))
            .And().ImplementInterface(typeof(ICommand<>))
            .And().ImplementInterface(typeof(IQuery<>))
            .Should()
            .BePublic()
            .GetResult();

        // Assert
        Assert.That(testResult.IsSuccessful);
    }
}