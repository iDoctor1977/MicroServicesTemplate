using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Models.AppModels.Wallets;
using CoreServicesTemplate.Dashboard.Web.Bases;
using CoreServicesTemplate.Dashboard.Web.Models.Wallets;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using Microsoft.AspNetCore.Mvc;

namespace CoreServicesTemplate.Dashboard.Web.Controllers
{
    public class WalletController : ControllerBase<WalletController>
    {
        private readonly ICustomMapper<CreateWalletViewModel, CreateWalletAppModel> _createWalletMapper;
        private readonly ICustomMapper<WalletViewModel, WalletAppModel> _walletMapper;

        private readonly ICreateWalletFeature _createWalletFeature;
        private readonly IGetWalletFeature _readWalletFeature;

        public WalletController(
            ICustomMapper<CreateWalletViewModel, CreateWalletAppModel> createWalletMapper,
            ICustomMapper<WalletViewModel, WalletAppModel> walletMapper,
            ICreateWalletFeature createWalletFeature,
            IGetWalletFeature readWalletFeature,
            ILogger<WalletController> logger) : base(logger)
        {
            _createWalletMapper = createWalletMapper;
            _walletMapper = walletMapper;
            _createWalletFeature = createWalletFeature;
            _readWalletFeature = readWalletFeature;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> Create(CreateWalletViewModel viewModel)
        {
            Logger.LogInformation("----- Create wallet items: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            var appModel = _createWalletMapper.Map(viewModel);

            if (ModelState.IsValid)
            {
                await _createWalletFeature.ExecuteAsync(appModel);
            }

            return RedirectToAction("Index");
        }

        public async Task<ViewResult> ReadWallet(Guid ownerGuid)
        {
            Logger.LogInformation("----- Read wallet items: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            var result = await _readWalletFeature.ExecuteAsync(ownerGuid);

            if (result.Value != null)
            {
                var viewModel = _walletMapper.Map(result.Value);

                return View(viewModel);
            }

            return View(Index());
        }
    }
}
