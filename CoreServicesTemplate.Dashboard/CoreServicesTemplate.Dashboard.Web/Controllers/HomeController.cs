using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Bases;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using Microsoft.AspNetCore.Mvc;

namespace CoreServicesTemplate.Dashboard.Web.Controllers
{
    public class HomeController : ControllerBase<HomeController>
    {
        private readonly IConsolidatorToData<UserViewModel, UserModel> _userConsolidator;
        private readonly IConsolidatorToData<UsersViewModel, UsersModel> _userCustomConsolidator;

        private readonly IAddUserFeature _addUserFeature;
        private readonly IGetUsersFeature _getUsersFeature;

        public HomeController(
            IConsolidatorToData<UserViewModel, UserModel> userReceiver,
            IConsolidatorToData<UsersViewModel, UsersModel> userCustomConsolidator,
            IAddUserFeature addUserFeature,
            IGetUsersFeature getUsersFeature,
            ILogger<HomeController> logger) : base(logger)
        {
            _userConsolidator = userReceiver;

            _addUserFeature = addUserFeature;
            _getUsersFeature = getUsersFeature;
            _userCustomConsolidator = userCustomConsolidator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> Add(UserViewModel viewModel)
        {
            var model = _userConsolidator.ToData(viewModel).Resolve();

            var responseMessage = new HttpResponseMessage();
            if (ModelState.IsValid)
            {
                responseMessage = await _addUserFeature.HandleAsync(model);
            }

            return RedirectToAction("Index", responseMessage);
        }

        [HttpGet]
        public async Task<ViewResult> GetAll()
        {
            var model = await _getUsersFeature.HandleAsync();

            var viewModel = _userCustomConsolidator.ToDataReverse(model).Resolve();

            return View(viewModel);
        }
    }
}
