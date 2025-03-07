using DailyNotesChart.Application.Abstractions.Persistance;
using DailyNotesChart.Application.Shared;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace DailyNotesChart.Persistance.Services;

internal sealed class EnsureDbPopulatedWithIdentityData : IEnsureDbPopulated
{
    private readonly RoleManager<IdentityRole<ApplicationUserId>> _roleManager;

    public EnsureDbPopulatedWithIdentityData(RoleManager<IdentityRole<ApplicationUserId>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task Ensure()
    {
        foreach (var role in Roles.GetAllRoles())
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole<ApplicationUserId>(role) { Id = new ApplicationUserId(Guid.NewGuid())});
            }
        }
    }
}