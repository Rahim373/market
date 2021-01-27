using Market.Catalog.Applications.Products.Manager;
using Market.Common.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Catalog.Applications.Products.Extensions
{
    public class ProductExtension : IInstallers
    {
        public void InstallApplicationServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IProductManager, ProductManager>();
        }
    }
}