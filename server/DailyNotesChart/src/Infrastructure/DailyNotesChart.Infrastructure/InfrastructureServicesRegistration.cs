using DailyNotesChart.Application.Abstractions.Identity;
using DailyNotesChart.Infrastructure.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DailyNotesChart.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITokenProvider, TokenProvider>();

        return services;
    }
}