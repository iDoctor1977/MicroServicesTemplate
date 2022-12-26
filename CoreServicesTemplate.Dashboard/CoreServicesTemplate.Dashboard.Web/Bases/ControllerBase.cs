using System.Diagnostics;
using CoreServicesTemplate.Dashboard.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreServicesTemplate.Dashboard.Web.Bases
{
    public class ControllerBase<T> : Controller where T : Controller
    {
        private readonly ILogger<T> _logger;

        public ControllerBase(ILogger<T> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
