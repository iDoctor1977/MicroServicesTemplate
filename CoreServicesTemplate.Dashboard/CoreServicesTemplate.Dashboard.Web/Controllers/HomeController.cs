using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Bases;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using Microsoft.AspNetCore.Mvc;

namespace CoreServicesTemplate.Dashboard.Web.Controllers
{
    public class HomeController : ControllerBase<HomeController>
    {
        private readonly IMapperService<UserViewModel, UserAppModel> _userCustomMapper;
        private readonly IMapperService<UsersViewModel, UsersAppModel> _usersCustomMapper;

        private readonly IAddUserFeature _addUserFeature;
        private readonly IGetUsersFeature _getUsersFeature;

        public HomeController(
            IMapperService<UserViewModel, UserAppModel> userCustomMapper,
            IMapperService<UsersViewModel, UsersAppModel> usersCustomMapper,
            IAddUserFeature addUserFeature,
            IGetUsersFeature getUsersFeature,
            ILogger<HomeController> logger) : base(logger)
        {
            _userCustomMapper = userCustomMapper;

            _addUserFeature = addUserFeature;
            _getUsersFeature = getUsersFeature;
            _usersCustomMapper = usersCustomMapper;
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
            var model = _userCustomMapper.Map(viewModel);

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

            var viewModel = _usersCustomMapper.Map(model);

            return View(viewModel);
        }
    }
}
