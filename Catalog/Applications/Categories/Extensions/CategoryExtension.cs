﻿using Market.Catalog.Applications.Categories.Manager;
using Market.Common.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Catalog.Applications.Categories.Extensions
{
    public class CategoryExtension : IInstallers
    {
        public void InstallApplicationServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICategoryManager, CategoryManager>();
        }
    }
}