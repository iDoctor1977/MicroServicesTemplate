using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.DomainModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots;

public class CreateWalletEventEfDepot : ICreateWalletEventEfDepot
{
    private readonly IDefaultMapper<CreateWalletEventModel, Wallet> _walletEventMapper;
    // private readonly IDefaultMapper<CreateWalletEventModel, User> _userMapper;
    // private readonly IDefaultMapper<CreateWalletEventModel, Info> _infoMapper;
    private readonly IWalletRepository _walletRepository;
    //private readonly IUserRepository _userRepository;
    //private readonly IInfoRepository _infoRepository;
    private readonly ILogger<CreateWalletEventEfDepot> _logger;

    public CreateWalletEventEfDepot(
        IDefaultMapper<CreateWalletEventModel, Wallet> walletMapper,
        IWalletRepository walletRepository, 
        ILogger<CreateWalletEventEfDepot> logger)
    {
        _walletEventMapper = walletMapper;
        _walletRepository = walletRepository;
        _logger = logger;
    }

    public async Task<OperationResult<CreateWalletEventModel>> ExecuteAsync(Guid ownerGuid)
    {
        _logger.LogInformation("----- Execute depot: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

        var entities = await _walletRepository.ReadForOwnerGuidAsync(ownerGuid);
        var walletEventModel =_walletEventMapper.Map(entities);

        // in this point you can read user info from repository and map here to return model
        walletEventModel.Name = "Filippo";
        walletEventModel.Surname = "Foglia";
        walletEventModel.Cap = "44123";
        walletEventModel.Address = "Via A. Magri, 12";
        walletEventModel.FromAddress = "info@wallet.com";

        // in this point you can read site info from repository and map here to return model
        walletEventModel.ToAddress = "fifoglia@gmail.com";
            
        return new OperationResult<CreateWalletEventModel>(walletEventModel);
    }
}