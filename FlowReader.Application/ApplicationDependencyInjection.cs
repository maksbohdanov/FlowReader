﻿using FlowReader.Application.Mapping;
using FlowReader.Application.Services;
using FlowReader.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FlowReader.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices();

            services.RegisterAutoMapper();

            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IFeedService, FeedService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IClaimService, ClaimService>();
        }

        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutomapperProfile));
        }
    }
}
