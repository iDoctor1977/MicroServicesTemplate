using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using CoreServicesTemplate.Shared.Core.Filters;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Core.Features;
using CoreServicesTemplate.StorageRoom.Data.MapperProfiles;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.Shared.Core.Mappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Api.MapperProfiles;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using System.Collections.Generic;
using CoreServicesTemplate.Shared.Core.Consolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.StorageRoom.Api.Consolidators;
using CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.AddUser;
using CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.GetUser;
using CoreServicesTemplate.StorageRoom.Data.Consolidators;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Repositories;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Mocks;
using CoreServicesTemplate.StorageRoom.Core.Interfaces;
using CoreServicesTemplate.StorageRoom.Core.Pipeline;

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

            services.AddTransient<IFeatureCommand<UserModel>, AddUserFeature>();
            services.AddTransient<IFeatureQuery<UserModel, UserModel>, GetUserFeature>();
            services.AddTransient<IFeatureQuery<UsersModel>, GetUsersFeature>();

            services.AddTransient<IAddUserDepot, AddUserEfDepot>();
            services.AddTransient<IGetUserDepot, GetUserEfDepot>();
            services.AddTransient<IGetUsersDepot, GetUsersEfDepot>();

            services.AddTransient<Lazy<StorageRoomDbContext>>();

            if (Configuration["mocked"]!.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                services.AddTransient<IUserRepository, UserEfRepositoryMock>();
            }
            else
            {
                services.AddTransient<IUserRepository, UserEfRepository>();
            }

            #endregion

            #region ConsolidatorReverse

            services.AddTransient<ICustomMapper, CustomMapper>();

            services.AddTransient(typeof(IConsolidatorToData<,>), typeof(DefaultConsolidator<,>));
            services.AddTransient(typeof(IConsolidatorToData<UsersApiModel, UsersModel>), typeof(UsersApiCustomConsolidator));
            services.AddTransient(typeof(IConsolidatorToData<UsersModel, IEnumerable<User>>), typeof(UsersDataCustomConsolidator));

            #endregion

            #region Filters

            services.AddControllers(options =>
            {
                options.Filters.Add<ApiLogActionFilterAsync>();
            });

            #endregion

            #region Automapper

            services.AddAutoMapper(typeof(ApiMappingProfile), typeof(DataMappingProfile));

            #endregion

            #region Pipeline FeatureCommand SubSteps

            services.AddTransient<IOperationsSupplier, OperationsSupplier>();

            services.AddTransient<AddUserStep1>();
            services.AddTransient<AddUserStep1SubStep1>();
            services.AddTransient<AddUserStep1SubStep2>();

            services.AddTransient<GetUserStep1>();
            services.AddTransient<GetUserStep1SubStep1>();

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
