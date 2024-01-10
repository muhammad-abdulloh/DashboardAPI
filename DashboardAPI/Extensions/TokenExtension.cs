using DashboardAPI.Services.TokenService;
using DashboardDBAccess.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DashboardAPI.Extensions
{
    public static class TokenExtension
    {
        public static IServiceCollection RegisterTokenConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenSettings>(configuration.GetSection("TokenConfiguration"));
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
