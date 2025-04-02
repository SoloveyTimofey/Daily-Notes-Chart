using DailyNotesChart.Application;
using DailyNotesChart.Persistance;
using DailyNotesChart.Domain;
using DailyNotesChart.Infrastructure;
using DailyNotesChart.WebApi.Extensions;
using System.Reflection;
using DailyNotesChart.Application.Abstractions.Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddDomainServices()
    .AddApplicationServices()
    .AddPersistenceServices(builder.Configuration)
    .AddInfrastructureServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly(), typeof(ApplicationServicesRegistration).Assembly);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ensurePopulatedWithIdentityData = scope.ServiceProvider.GetRequiredService<IEnsureDbPopulatedWithNecessaryData>();

    await ensurePopulatedWithIdentityData.Ensure();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();