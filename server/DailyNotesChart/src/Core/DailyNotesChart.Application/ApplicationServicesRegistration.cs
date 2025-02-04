using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using DailyNotesChart.Application.Behaviors;

namespace DailyNotesChart.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicatiomServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            config.AddOpenBehavior(typeof(UnitOfWorkBehaior<,>));
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}