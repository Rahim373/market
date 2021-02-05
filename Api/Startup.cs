using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Reflection;
using Market.Application.Interfaces;
using Market.Infrastructure.Persistence;

namespace Market.Catalog.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private const string corsPolicy = "local-react";

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            });

            var assemblies = Assembly.GetAssembly(typeof(Startup)).GetReferencedAssemblies()
                .Where(x => x.Name.StartsWith("Market.Applications"))
                .Select(x => Assembly.Load(x.Name))
                .ToArray();
            services.AddMediatR(assemblies);

            services.AddCors(options =>
            {
                options.DefaultPolicyName = corsPolicy;
                options.AddPolicy(corsPolicy, builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors(corsPolicy);
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            dbContext.DB.MigrateAsync().Wait();
        }
    }
}