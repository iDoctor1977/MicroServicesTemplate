using System.Net.Http;
using System.Threading.Tasks;
using CoreServicesTemplate.Console.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Console.Common.Models;
using CoreServicesTemplate.Console.Web.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Console.Web.Controllers
{
    public class HomeController : BaseController<HomeController>
    {
        private readonly IConsolidators<UserViewModel, UserModel> _createUserCustomReceiver;
        private readonly IConsolidators<UsersModel, UsersViewModel> _readUsersCustomPresenter;

        private readonly IAddUserFeature _addUserFeature;
        private readonly IGetUsersFeature _getUsersFeature;

        public HomeController(
            IConsolidators<UserViewModel, UserModel> userReceiver,
            IConsolidators<UsersModel, UsersViewModel> userPresenter,
            IAddUserFeature addUserFeature,
            IGetUsersFeature getUsersFeature,
            ILogger<HomeController> logger) : base(logger)
        {
            _createUserCustomReceiver = userReceiver;
            _readUsersCustomPresenter = userPresenter;

            _addUserFeature = addUserFeature;
            _getUsersFeature = getUsersFeature;
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
                responseMessage = await _addUserFeature.HandleAsync(model);
            }

            return RedirectToAction("Index", responseMessage);
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var model = await _getUsersFeature.HandleAsync();

            var viewModel = _readUsersCustomPresenter.ToData(model);

            return View(viewModel);
        }
    }
}
