using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Console.Web.Controllers
{
    public class BaseController<T> : Controller where T : Controller
    {
        private readonly ILogger<T> _logger;

        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }
    }
}
