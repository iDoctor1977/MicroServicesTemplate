using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using CoreServicesTemplate.Console.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Console.Web.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Console.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConsolidators<UserViewModel, UserApiModel> _createUserCustomReceiver;
        private readonly IConsolidators<UserApiModel, UserViewModel> _readUserCustomPresenter;

        private readonly ICreateUserFeature _createUserFeature;

        public HomeController(IServiceProvider service, ILogger<HomeController> logger)
        {
            _logger = logger;

            _createUserCustomReceiver = service.GetRequiredService<IConsolidators<UserViewModel, UserApiModel>>();
            _readUserCustomPresenter = service.GetRequiredService<IConsolidators<UserApiModel, UserViewModel>>();

            _createUserFeature = service.GetRequiredService<ICreateUserFeature>();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel viewModel)
        {
            var model = _createUserCustomReceiver.ToData(viewModel);

            var responseMessage = new HttpResponseMessage();
            if (ModelState.IsValid)
            {
                responseMessage = await _createUserFeature.ExecuteAsync(model);
            }

            return RedirectToAction("Index", responseMessage);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
