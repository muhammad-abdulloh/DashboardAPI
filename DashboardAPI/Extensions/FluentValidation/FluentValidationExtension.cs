﻿using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DashboardAPI.Extensions.FluentValidation
{
    public static class FluentValidationExtension
    {
        public static IServiceCollection RegisterFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(config =>
            {
                config.DisableDataAnnotationsValidation = true;
            });
            services.AddMvc(options => { options.Filters.Add(typeof(ValidatorFilterAttribute)); });
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            return services;
        }
    }
}
