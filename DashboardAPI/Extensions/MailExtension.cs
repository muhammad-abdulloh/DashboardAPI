using DashboardAPI.Models.Settings;
using DashboardAPI.Services.MailService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MailService = DashboardAPI.Services.MailService.EmailService;


namespace DashboardAPI.Extensions;

public static class MailExtensions
{
    public static IServiceCollection RegisterMailService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailConfigurationSettings>(configuration.GetSection(EmailConfigurationSettings.Position));
        services.AddScoped<IEmailService, MailService>();
        return services;
    }
    
}