﻿using CoreServicesTemplate.Dashboard.Common.Interfaces.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace CoreServicesTemplate.Dashboard.Api.Testing.Fixtures
{
    public class ApiCustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        public Mock<IStorageRoomService> StorageRoomServiceMock { get; }

        public ApiCustomWebApplicationFactory()
        {
            StorageRoomServiceMock = new Mock<IStorageRoomService>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Replace(new ServiceDescriptor(typeof(IStorageRoomService), StorageRoomServiceMock.Object));
            });

            builder.UseEnvironment("Development");
        }
    }
}
