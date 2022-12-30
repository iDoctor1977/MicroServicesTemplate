using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Bases;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using Microsoft.AspNetCore.Mvc;

namespace CoreServicesTemplate.Dashboard.Web.Controllers
{
    public class HomeController : ControllerBase<HomeController>
    {
        private readonly IConsolidator<UserViewModel, UserModel> _userConsolidator;
        private readonly IConsolidator<UsersViewModel, UsersModel> _userCustomConsolidator;

        private readonly IFeatureCommand<UserModel> _addUserFeature;
        private readonly IFeatureQuery<UsersModel> _getUsersFeature;

        public HomeController(
            IConsolidator<UserViewModel, UserModel> userReceiver,
            IConsolidator<UsersViewModel, UsersModel> userCustomConsolidator,
            IFeatureCommand<UserModel> addUserFeature,
            IFeatureQuery<UsersModel> getUsersFeature,
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

            if (ModelState.IsValid)
            {
                await _addUserFeature.SetAggregate(model).HandleAsync();
            }

            return RedirectToAction("Index");
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
