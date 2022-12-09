using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreServicesTemplate.Shared.Core.Filters
{
    public class ApiLogActionFilterAsync : IAsyncActionFilter
    {
        private readonly ILogger<ApiLogActionFilterAsync> _logger;

        public ApiLogActionFilterAsync(ILogger<ApiLogActionFilterAsync> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Do something before the action executes.
            var logApiModel = new LogApiModel
            {
                LogTime = DateTime.Now,
                IpAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString(),
                Request = context.HttpContext.Request.ToString(),
                Body = context.HttpContext.Request.Body.ToString()
            };

            _logger.LogInformation($"StorageRoom API call STARTED - {JsonConvert.SerializeObject(logApiModel)}");
            
            await next();
        }
    }
}
