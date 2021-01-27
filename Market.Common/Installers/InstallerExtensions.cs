using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Common.Installers
{
    public static class InstallerExtensions
    {
        public static void InstallServices(this IServiceCollection services, IConfiguration configuration, Type type)
        {
            AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.ExportedTypes)
                .Where(c => typeof(IInstallers).IsAssignableFrom(c) && !c.IsAbstract)
                .Select(Activator.CreateInstance)
                .ToList()
                .Cast<IInstallers>()
                .ToList()
                .ForEach(installer => installer.InstallApplicationServices(services, configuration));
        }
    }
}