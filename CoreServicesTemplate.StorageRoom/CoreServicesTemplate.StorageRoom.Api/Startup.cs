using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using CoreServicesTemplate.Shared.Core.Filters;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Core.Features;
using CoreServicesTemplate.StorageRoom.Data.DepotsEF;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.MapperProfiles;
using CoreServicesTemplate.StorageRoom.Data.Mocks;
using CoreServicesTemplate.StorageRoom.Data.RepositoriesEF;
using CoreServicesTemplate.StorageRoom.Data.RepositoriesEF.Interfaces;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.Shared.Core.Mappers;
using CoreServicesTemplate.Shared.Core.Presenters;
using CoreServicesTemplate.Shared.Core.Receivers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Api.MapperProfiles;
using CoreServicesTemplate.StorageRoom.Api.Presenters;
using CoreServicesTemplate.StorageRoom.Api.Receivers;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using System.Collections.Generic;
using CoreServicesTemplate.StorageRoom.Data.Presenters;

namespace CoreServicesTemplate.StorageRoom.Api
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

            services.AddTransient<IAddUserFeature, AddUserFeature>();
            services.AddTransient<IGetUserFeature, GetUserFeature>();
            services.AddTransient<IGetUsersFeature, GetUsersFeature>();

            services.AddTransient<IAddUserDepot, AddUserDepotEF>();
            services.AddTransient<IGetUserDepot, GetUserDepotEF>();
            services.AddTransient<IGetUsersDepot, GetUsersDepotEF>();

            services.AddTransient<IRepositoryFactoryEF, RepositoryFactoryEF>();
            services.AddTransient<IUserRepository, UserRepositoryEF>();

            services.AddTransient<Lazy<DbContextProject>>();

            if (Configuration["mocked"]!.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                services.AddTransient<IUserRepository, UserRepositoryEFMock>();
            }
            else
            {
                services.AddTransient<IUserRepository, UserRepositoryEF>();
            }

            #endregion

            #region Consolidator

            services.AddTransient<ICustomMapper, CustomMapper>();

            services.AddTransient(typeof(IConsolidators<,>), typeof(DefaultReceiver<,>));
            services.AddTransient(typeof(IConsolidators<,>), typeof(DefaultPresenter<,>));

            services.AddTransient(typeof(IConsolidators<UsersApiModel, UsersModel>), typeof(GetUsersApiCustomReceiver));
            
            services.AddTransient(typeof(IConsolidators<UsersModel, UsersApiModel>), typeof(GetUsersApiCustomPresenter));
            services.AddTransient(typeof(IConsolidators<IEnumerable<User>, UsersModel>), typeof(GetUsersDataCustomPresenter));

            #endregion

            #region Filters

            services.AddControllers(options =>
            {
                options.Filters.Add<ApiLogActionFilterAsync>();
            });

            #endregion

            #region Automapper

            services.AddAutoMapper(typeof(CoreMappingProfile), typeof(DataMappingProfile));

            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoreServicesTemplate.StorageRoom.Api", Version = "v1" });
            });

            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreServicesTemplate.StorageRoom.Api v1"));
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
