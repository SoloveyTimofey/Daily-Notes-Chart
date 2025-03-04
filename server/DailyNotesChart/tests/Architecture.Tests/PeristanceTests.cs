using DailyNotesChart.Persistance;

namespace Architecture.Tests;

[TestFixture]
public class PeristanceTests
{
    [Test]
    public void PersistanceClasses_ShouldNot_BePublic()
    {
        // Act
        var testResult = Types.InAssembly(typeof(PersistanceServicesRegistration).Assembly)
            .That()
            .AreClasses()
            .And().DoNotHaveName(nameof(PersistanceServicesRegistration))
            .And().DoNotResideInNamespaceContaining("Migrations")
            .ShouldNot()
            .BePublic()
            .GetResult();

        // Assert
        Assert.That(testResult.IsSuccessful);
    }
}