using CoreServicesTemplate.Console.Core;
using CoreServicesTemplate.Console.Core.MapperProfiles;
using CoreServicesTemplate.Console.Web.MapperProfiles;
using CoreServicesTemplate.Console.Web.Models;
using CoreServicesTemplate.Console.Web.Presenters;
using CoreServicesTemplate.Console.Web.Receivers;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.Shared.Core.Mappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.Shared.Core.Receivers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreServicesTemplate.Console.Web
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
            #region Injection

            CoreConfigureServices.InitializeDependencies(services);

            #endregion

            #region Consolidator

            services.AddTransient<ICustomMapper, CustomMapper>();

            services.AddTransient(typeof(IConsolidators<,>), typeof(DefaultReceiver<,>));
            services.AddTransient(typeof(IConsolidators<UserViewModel, UserApiModel>), typeof(CreateUserCustomReceiver));
            services.AddTransient(typeof(IConsolidators<UserApiModel, UserViewModel>), typeof(ReadUserCustomPresenter));
            services.AddTransient(typeof(IConsolidators<UsersApiModel, UsersViewModel>), typeof(ReadUsersCustomPresenter));

            #endregion

            #region Automapper

            services.AddAutoMapper(typeof(WebMappingProfile), typeof(CoreMappingProfile));

            #endregion

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
