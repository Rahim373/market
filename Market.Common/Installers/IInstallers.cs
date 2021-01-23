using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Common.Installers
{
    public interface IInstallers
    {
        void InstallApplicationServices(IServiceCollection services, IConfiguration configuration);
    }
}