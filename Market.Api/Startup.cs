using Market.Common.Installers;
using Market.Domain.Context;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Reflection;

namespace Market.Catalog.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<MarketDbContext>(options =>
                options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"))
            );

            var assemblies = Assembly.GetAssembly(typeof(Startup)).GetReferencedAssemblies()
                .Where(x => x.Name.StartsWith("Market.Applications"))
                .Select(x => Assembly.Load(x.Name))
                .ToArray();
            services.AddMediatR(assemblies);

            services.InstallServices(_configuration, typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MarketDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


            dbContext.Database.MigrateAsync().Wait();
        }
    }
}