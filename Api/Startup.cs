using Market.Api.Services;
using Market.Application.DI;
using Market.Application.Interfaces;
using Market.Infrastructure.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            services.InstallApplication(_configuration);
            services.InstallInfrastructure(_configuration);

            services.AddTransient<ICurrentUserService, CurrentUserService>();

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