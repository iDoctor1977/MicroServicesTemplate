using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using CoreServicesTemplate.Console.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Console.Web.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Console.Web.Controllers
{
    public class HomeController : BaseController<HomeController>
    {
        private readonly IConsolidators<UserViewModel, UserApiModel> _createUserCustomReceiver;
        private readonly IConsolidators<UsersApiModel, UsersViewModel> _readUsersCustomPresenter;

        private readonly ICreateUserFeature _createUserFeature;
        private readonly IReadUsersFeature _readUsersFeature;

        public HomeController(
            IConsolidators<UserViewModel, UserApiModel> userReceiver,
            IConsolidators<UsersApiModel, UsersViewModel> userPresenter,
            ICreateUserFeature createUserFeature,
            IReadUsersFeature readUsersFeature,
            ILogger<HomeController> logger) : base(logger)
        {
            _createUserCustomReceiver = userReceiver;
            _readUsersCustomPresenter = userPresenter;

            _createUserFeature = createUserFeature;
            _readUsersFeature = readUsersFeature;
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
                responseMessage = await _createUserFeature.HandleAsync(model);
            }

            return RedirectToAction("Index", responseMessage);
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var model = await _readUsersFeature.HandleAsync();

            var viewModel = _readUsersCustomPresenter.ToData(model);

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
