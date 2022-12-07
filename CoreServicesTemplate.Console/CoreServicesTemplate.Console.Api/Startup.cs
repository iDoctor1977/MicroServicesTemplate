using CoreServicesTemplate.Console.Common.Models;
using CoreServicesTemplate.Console.Core;
using CoreServicesTemplate.Console.Core.MapperProfiles;
using CoreServicesTemplate.Console.Core.Presenters;
using CoreServicesTemplate.Console.Core.Receivers;
using CoreServicesTemplate.Shared.Core.HealthChecks;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.Shared.Core.Mappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.Shared.Core.Presenters;
using CoreServicesTemplate.Shared.Core.Receivers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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

            CoreConfigureServices.InitializeDependencies(services);

            services.AddHttpClient();

            #endregion

            #region Consolidator

            services.AddTransient<ICustomMapper, CustomMapper>();

            services.AddTransient(typeof(IConsolidators<,>), typeof(DefaultReceiver<,>));
            services.AddTransient(typeof(IConsolidators<,>), typeof(DefaultPresenter<,>));

            services.AddTransient(typeof(IConsolidators<UsersApiModel, UsersModel>), typeof(GetUsersCoreCustomReceiver));

            services.AddTransient(typeof(IConsolidators<UsersModel, UsersApiModel>), typeof(GetUsersCoreCustomPresenter));

            #endregion

            #region Automapper

            services.AddAutoMapper(typeof(CoreMappingProfile));

            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoreServicesTemplate.Consolle.Api", Version = "v1" });
            });

            services.AddHealthChecks().AddCheck<StorageRoomHealthCheck>("StorageRooma API service");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreServicesTemplate.Consolle.Api v1"));
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
