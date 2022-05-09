using CoreServicesTemplate.Console.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Console.Core.Features;
using CoreServicesTemplate.Shared.Core.HealthChecks;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.Shared.Core.QueueMessages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.Transport.FileSystem;

namespace CoreServicesTemplate.Console.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Injections

            services.AddTransient<IHealthCheck, StorageRoomHealthCheck>();

            services.AddTransient<IReadUsersFeature, ReadUsersFeature>();
            services.AddTransient<ISimulationRebusFeature, SimulationRebusFeature>();

            #endregion

            #region Rebus Data streaming

            services.AddRebus((configure) => configure
                .Logging(l => l.ColoredConsole())
                .Transport(t => t.UseFileSystem("c:/temp/rebus", "queueConsole"))
                .Routing(r => r.TypeBased().Map<SimulationMessage>("queueStorageRoom"))
            );

            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoreServicesTemplate.Console.Api", Version = "v1" });
            });

            services.AddHealthChecks().AddCheck<StorageRoomHealthCheck>("StorageRoom API service");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreServicesTemplate.Console.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
