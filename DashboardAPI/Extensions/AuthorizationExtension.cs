using DashboardAPI.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace DashboardAPI.Extensions
{
    public static class AuthorizationExtension
    {
        public static IServiceCollection RegisterAuthorization(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.RegisterAuthorizationHandlers();
            return services;
        }
    }
}
