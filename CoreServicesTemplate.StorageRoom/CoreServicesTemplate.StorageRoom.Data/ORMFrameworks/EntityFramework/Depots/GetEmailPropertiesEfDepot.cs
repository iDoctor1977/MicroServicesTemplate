using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models.DomainModels.Wallet;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots;

public class GetEmailPropertiesEfDepot : IGetEmailPropertiesEfDepot
{
    private readonly IDefaultMapper<EmailPropertiesModel, Wallet> _walletEventMapper;
    // private readonly IDefaultMapper<CreateWalletEventModel, User> _userMapper;
    // private readonly IDefaultMapper<CreateWalletEventModel, Info> _infoMapper;
    private readonly IWalletRepository _walletRepository;
    //private readonly IUserRepository _userRepository;
    //private readonly IInfoRepository _infoRepository;
    private readonly ILogger<GetEmailPropertiesEfDepot> _logger;

    public GetEmailPropertiesEfDepot(
        IDefaultMapper<EmailPropertiesModel, Wallet> walletMapper,
        IWalletRepository walletRepository, 
        ILogger<GetEmailPropertiesEfDepot> logger)
    {
        _walletEventMapper = walletMapper;
        _walletRepository = walletRepository;
        _logger = logger;
    }

    public async Task<OperationResult<EmailPropertiesModel>> ExecuteAsync(Guid ownerGuid)
    {
        _logger.LogInformation("----- Execute depot: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

        var entities = await _walletRepository.ReadForOwnerGuidAsync(ownerGuid);
        var model =_walletEventMapper.Map(entities);

        // in this point you can read user info from specific repository and map here to return model
        model.Name = "Filippo";
        model.Surname = "Foglia";
        model.Cap = "44123";
        model.Address = "Via A. Magri, 12";
        model.FromAddress = "info@wallet.com";

        // in this point you can read site info from specific repository and map here to return model
        model.ToAddress = "fifoglia@gmail.com";
            
        return new OperationResult<EmailPropertiesModel>(model);
    }
}