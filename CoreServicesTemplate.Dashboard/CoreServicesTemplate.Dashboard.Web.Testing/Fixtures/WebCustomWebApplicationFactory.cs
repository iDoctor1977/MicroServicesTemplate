using CoreServicesTemplate.Dashboard.Common.Interfaces.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace CoreServicesTemplate.Dashboard.Web.Testing.Fixtures
{
    public class WebCustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        public Mock<ICreateWalletService> CreateWalletServiceMock { get; }
        public Mock<IGetWalletService> GetWalletServiceMock { get; }

        public WebCustomWebApplicationFactory()
        {
            CreateWalletServiceMock = new Mock<ICreateWalletService>();
            GetWalletServiceMock = new Mock<IGetWalletService>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Replace(new ServiceDescriptor(typeof(ICreateWalletService), CreateWalletServiceMock.Object));
                services.Replace(new ServiceDescriptor(typeof(IGetWalletService), GetWalletServiceMock.Object));
            });

            builder.UseEnvironment("Development");
        }
    }
}
