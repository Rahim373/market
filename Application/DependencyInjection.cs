using Market.Application.Categories.Interfaces;
using Market.Application.Categories.Services;
using Market.Application.Products.Interfaces;
using Market.Application.Products.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Market.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection InstallApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICategoryManager, CategoryManager>();
            services.AddTransient<IProductManager, ProductManager>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}