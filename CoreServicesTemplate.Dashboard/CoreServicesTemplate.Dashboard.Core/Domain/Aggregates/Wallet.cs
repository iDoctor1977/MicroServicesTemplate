using CoreServicesTemplate.Dashboard.Common.Models.DomainModels.WalletItems;
using CoreServicesTemplate.Dashboard.Common.Models.DomainModels.Wallets;
using CoreServicesTemplate.Dashboard.Core.Domain.Entities;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Exceptions;
using CoreServicesTemplate.Shared.Core.Interfaces.IFactories;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Interfaces.IModels;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Dashboard.Core.Domain.Aggregates
{
    public class Wallet : EntityBase<WalletModel, Wallet>
    {
        public Guid OwnerGuid { get; private set; }
        public decimal TradingAllowedBalance { get; private set; }
        public decimal OperationAllowedBalance { get; private set; }
        public decimal Balance { get; private set; }
        public decimal Performance { get; private set; }
        public ICollection<WalletItem> WalletItems { get; private set; }

        #region Domain aggregate construction instance

        private Wallet(
            IDomainEntityFactory factoryBase,
            IDefaultMapper<WalletModel, Wallet> mapperBase,
            ILogger<Wallet> loggerBase) : base(factoryBase, mapperBase, loggerBase)
        {
            WalletItems = new List<WalletItem>();
        }

        /// <summary>
        /// To create instance
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="createMapper"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="model"></param>
        public Wallet(
            IDomainEntityFactory factory,
            IDefaultMapper<CreateWalletModel, Wallet> createMapper,
            IDefaultMapper<WalletModel, Wallet> mapper,
            ILogger<Wallet> logger,
            CreateWalletModel model) : this(factory, mapper, logger)
        {
            SharedConstruction(model);

            createMapper.Map(model, this);
        }

        /// <summary>
        /// To update instance
        /// </summary>
        /// <param name="factoryBase"></param>
        /// <param name="mapperBase"></param>
        /// <param name="loggerBase"></param>
        /// <param name="model"></param>
        /// <exception cref="DomainValidationException{T}"></exception>
        public Wallet(
            IDomainEntityFactory factoryBase,
            IDefaultMapper<WalletModel, Wallet> mapperBase,
            ILogger<Wallet> loggerBase,
            WalletModel model) : this(factoryBase, mapperBase, loggerBase)
        {
            SharedConstruction(model);

            try
            {
                WalletItems = model.WalletItems.Select(FactoryBase.Generate<WalletItemModel, WalletItem>).ToList();
            }
            catch (DomainValidationException<WalletItem> e)
            {
                LoggerBase.LogCritical($"{GetType().Name}: {e.Message}");

                throw new DomainValidationException<Wallet>($"{GetType().Name}: wallet item generation failed", e);
            }

            MapperBase.Map(model, this);
        }

        protected override void SharedConstruction(IModelBase modelBase)
        {
            if (modelBase is WalletModelBase model)
            {
                if (model.Balance <= 0)
                {
                    throw new DomainValidationException<Wallet>($"{nameof(model.Balance)} is not valid");
                }
                if (model.OwnerGuid == Guid.Empty || model.Equals(null))
                {
                    throw new DomainValidationException<Wallet>($"{nameof(model.OwnerGuid)} is not valid");
                }
                if (model.TradingAllowedBalance <= 0)
                {
                    throw new DomainValidationException<Wallet>($"{nameof(model.TradingAllowedBalance)} is not valid");
                }
                if (model.OperationAllowedBalance <= 0)
                {
                    throw new DomainValidationException<Wallet>($"{nameof(model.OperationAllowedBalance)} is not valid");
                }
            }
            else
            {
                throw new DomainValidationException<Wallet>($"{nameof(model)} is not valid");
            }
        }

        #endregion

        public override WalletModel ToModel()
        {
            var toModel = MapperBase.Map(this);

            return toModel;
        }
    }
}
