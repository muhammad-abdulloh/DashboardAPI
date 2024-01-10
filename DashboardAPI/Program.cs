using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DashboardAPI;
using DashboardAPI.Extensions;
using DashboardAPI.Extensions.FluentValidation;
using DashboardAPI.Models.Settings;
using DashboardDBAccess;
using DashboardDBAccess.Data;
using DashboardDBAccess.DataContext;
using DashboardDBAccess.Extensions;
using DashboardDBAccess.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Host.RegisterLoggerConfiguration();
builder.Services.AddHttpContextAccessor();
builder.Services.RegisterFluentValidation();
builder.Services.RegisterDatabaseProvider(configuration);
builder.Services.RegisterIdentity();

builder.Services.RegisterRepositoryServices();
builder.Services.RegisterResourceServices();
builder.Services.RegisterDtoResourceValidators();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.RegisterMailService(configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterSwagger();

builder.Services.RegisterAuthorization();

builder.Services.RegisterTokenConfiguration(configuration);
var jwtSettings = configuration.GetSection("TokenConfiguration").Get<TokenSettings>();
builder.Services.RegisterAuthentication(jwtSettings);

var logSettings = configuration.GetSection(SerilogSettings.Position).Get<SerilogSettings>();
if (!string.IsNullOrEmpty(logSettings?.MinimumLevel?.Default) && 
    (string.Equals(logSettings.MinimumLevel.Default, "debug", StringComparison.OrdinalIgnoreCase) ||
    string.Equals(logSettings.MinimumLevel.Default, "verbose", StringComparison.OrdinalIgnoreCase)))
{
    builder.Services.AddAllHttpLoggingInformationAvailable();
}

var app = builder.Build();

app.UseExceptionHandler("/error");
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseHsts();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Raqamli Yordamchi Dashboard V1");
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseHttpLogging()
    .UseSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await FillInDatabase();

app.Run();

async Task FillInDatabase()
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DashboardDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    await DbInitializer.SeedWithDefaultValues(context, roleManager, userManager);
}

namespace DashboardAPI
{
    public partial class Program { }
}
