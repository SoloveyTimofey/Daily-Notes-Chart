using DailyNotesChart.Application;
using DailyNotesChart.Domain;
using DailyNotesChart.Infrastructure;
using DailyNotesChart.Persistance;
using DailyNotesChart.WebApi.Profiles;

namespace Architecture.Tests;

[TestFixture]
public class ArchitectureGlobalTests
{
    private const string DOMAIN_NAMESPACE = "DailyNotesChart.Domain";
    private const string APPLICATION_NAMESPACE = "DailyNotesChart.Application";
    private const string INFRASTRUCTURE_NAMESPACE = "DailyNotesChart.Infrastructure";
    private const string PERSISTANCE_NAMESPACE = "DailyNotesChart.Persistance";
    private const string WEB_API_NAMESPACE = "DailyNotesChart.WebApi";

    [Test]
    public void Domain_ShouldNot_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(DomainServicesRegistration).Assembly;

        var otherProjects = new[]
        {
            APPLICATION_NAMESPACE,
            INFRASTRUCTURE_NAMESPACE,
            WEB_API_NAMESPACE,
            PERSISTANCE_NAMESPACE
        };

        // Act
        var testResult = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        Assert.That(testResult.IsSuccessful);
    }

    [Test]
    public void Application_ShouldNot_HaveDependencyOnOtherProjectsExceptDomain()
    {
        // Arrange
        var assembly = typeof(ApplicationServicesRegistration).Assembly;

        var otherProjects = new[]
        {
            INFRASTRUCTURE_NAMESPACE,
            WEB_API_NAMESPACE,
            PERSISTANCE_NAMESPACE
        };

        // Act
        var testResult = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        Assert.That(testResult.IsSuccessful);
    }

    [Test]
    public void Infrastructure_ShouldNot_HaveDependencyOnWebApiProjects()
    {
        // Arrange
        var assembly = typeof(InfrastructureServicesRegistration).Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .Should()
            .NotHaveDependencyOn(WEB_API_NAMESPACE)
            .GetResult();

        // Assert
        Assert.That(testResult.IsSuccessful);
    }

    [Test]
    public void Persistance_ShouldNot_HaveDependencyOnWebApiAndInfrastructureProjects()
    {
        // Arrange
        var assembly = typeof(PersistenceServicesRegistration).Assembly;

        var otherProjects = new[]
        {
            WEB_API_NAMESPACE,
            INFRASTRUCTURE_NAMESPACE,
        };

        // Act
        var testResult = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        Assert.That(testResult.IsSuccessful);
    }

    [Test]
    public void WebApi_ShouldNot_HaveDependencyOnInfrastructureAndPersistancExceptAllowedTypes()
    {
        // Arrange
        var assembly = typeof(ChartRequestsToCommandsProfile).Assembly;

        var forbiddenNamespaces = new[]
        {
            INFRASTRUCTURE_NAMESPACE,
            PERSISTANCE_NAMESPACE
        };

        var allowedTypes = new[]
        {
            typeof(InfrastructureServicesRegistration).FullName,
            typeof(PersistenceServicesRegistration).FullName,
            "DailyNotesChart.Persistance.Contexts.DailyNotesChartWriteDbContext"
        };

        // Act
        var testResult = Types.InAssembly(assembly)
            .That()
            .ResideInNamespace(WEB_API_NAMESPACE)
            .ShouldNot()
            .HaveDependencyOnAll(forbiddenNamespaces)
            .GetResult();

        // Assert
        Assert.That(testResult.IsSuccessful);
    }
}