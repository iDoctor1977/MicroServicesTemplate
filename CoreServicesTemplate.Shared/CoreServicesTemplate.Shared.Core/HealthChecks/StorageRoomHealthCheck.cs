using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CoreServicesTemplate.Shared.Core.HealthChecks
{
    public class StorageRoomHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public StorageRoomHealthCheck(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var httpClient = _clientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(ApiUrl.StorageRoomApi.StorageRoomUrlBase());

            try
            {
                var response = await httpClient.GetAsync($"{ApiUrl.StorageRoomApi.GetHealthy()}", cancellationToken);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var contents = response.Content.ReadAsStringAsync(cancellationToken).Result;

                    if (contents.Equals("healthy", StringComparison.OrdinalIgnoreCase))
                    {
                        return HealthCheckResult.Healthy("StorageRoomApi ApiUrl endpoint is healthy");
                    }
                }

                return HealthCheckResult.Degraded("StorageRoomApi ApiUrl endpoint is degraded");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return HealthCheckResult.Degraded("StorageRoomApi ApiUrl endpoint is unhealthy");
            }
        }
    }
}