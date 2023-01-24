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
using CoreServicesTemplate.Shared.Core.Mappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Api.MapperProfiles;
using CoreServicesTemplate.StorageRoom.Common.Models;
using System.Collections.Generic;
using CoreServicesTemplate.Shared.Core.Consolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Api.Consolidators;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.AddUser;
using CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.GetUser;
using CoreServicesTemplate.StorageRoom.Data.Consolidators;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Repositories;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Mocks;
using CoreServicesTemplate.StorageRoom.Core.Interfaces;
using CoreServicesTemplate.StorageRoom.Core;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Interfaces;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.MappingProfiles;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.UserAggregate;
using CoreServicesTemplate.StorageRoom.Core.Consolidators;
using CoreServicesTemplate.StorageRoom.Core.MappingProfiles;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using Microsoft.EntityFrameworkCore;

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

            services.AddTransient<IAddUserDepot, AddUserEfDepot>();
            services.AddTransient<IGetUserDepot, GetUserEfDepot>();
            services.AddTransient<IGetUsersDepot, GetUsersEfDepot>();

            if (Configuration["mocked"]!.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                services.AddTransient<IUserRepository, UserEfRepositoryMock>();
            }
            else
            {
                services.AddTransient<IUserRepository, UserEfRepository>();
            }

            #endregion

            #region Db provider connection string

            if (Configuration["DBProvider"]!.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                // only for SQLite
                services.AddDbContext<StorageRoomDbContext>();
            }
            else
            {
                // only for SQLServer
                services.AddDbContext<StorageRoomDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("StorageRoomDB")));
            }

            #endregion

            #region Domain Aggregates

            services.AddTransient<IUserAggregateRoot, UserAggregate>();
            services.AddTransient<IAddressItem, AddressItem>();

            #endregion

            #region Consolidator

            services.AddTransient(typeof(IConsolidator<,>), typeof(DefaultConsolidator<,>));
            services.AddTransient(typeof(IConsolidator<UserApiModel, UserAppModel>), typeof(UserApiCustomConsolidator));
            services.AddTransient(typeof(IConsolidator<UsersApiModel, UsersAppModel>), typeof(UsersApiCustomConsolidator));

            services.AddTransient(typeof(IConsolidator<UserAppModel, UserAggModel>), typeof(UserCoreCustomConsolidator));

            services.AddTransient(typeof(IConsolidator<UsersAppModel, IEnumerable<User>>), typeof(UsersDataCustomConsolidator));

            #endregion

            #region Filters

            services.AddControllers(options =>
            {
                options.Filters.Add<ApiLogActionFilterAsync>();
            });

            #endregion

            #region Automapper

            services.AddTransient<ICustomMapper, CustomMapper>();
            services.AddAutoMapper(typeof(ApiMappingProfile), typeof(DataMappingProfile), typeof(FeatureMappingProfile), typeof(AggregateMappingProfile));

            #endregion

            #region Pipeline FeatureCommand Sub Steps

            services.AddTransient<ISubStepSupplier, SubStepSupplier>();

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
