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
        private readonly IConsolidator<UserViewModel, UserAppModel> _userCustomConsolidator;
        private readonly IConsolidator<UsersViewModel, UsersAppModel> _usersCustomConsolidator;

        private readonly IFeatureCommand<UserAppModel> _addUserFeature;
        private readonly IFeatureQuery<UsersAppModel> _getUsersFeature;

        public HomeController(
            IConsolidator<UserViewModel, UserAppModel> userCustomConsolidator,
            IConsolidator<UsersViewModel, UsersAppModel> usersCustomConsolidator,
            IFeatureCommand<UserAppModel> addUserFeature,
            IFeatureQuery<UsersAppModel> getUsersFeature,
            ILogger<HomeController> logger) : base(logger)
        {
            _userCustomConsolidator = userCustomConsolidator;

            _addUserFeature = addUserFeature;
            _getUsersFeature = getUsersFeature;
            _usersCustomConsolidator = usersCustomConsolidator;
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
            var model = _userCustomConsolidator.ToData(viewModel).Resolve();

            if (ModelState.IsValid)
            {
                await _addUserFeature.HandleAsync(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ViewResult> GetAll()
        {
            var model = await _getUsersFeature.HandleAsync();

            var viewModel = _usersCustomConsolidator.ToDataReverse(model).Resolve();

            return View(viewModel);
        }
    }
}
