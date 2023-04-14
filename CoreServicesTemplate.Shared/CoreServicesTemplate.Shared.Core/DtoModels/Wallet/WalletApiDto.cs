using System.Collections.Generic;
using CoreServicesTemplate.Shared.Core.DtoModels.WalletItem;

namespace CoreServicesTemplate.Shared.Core.DtoModels.Wallet
{
    public class WalletApiDto : WalletApiBaseDto
    {
        public ICollection<WalletItemApiDto> WalletItems { get; set; }
    }
}