using System.Collections.Generic;
using CoreServicesTemplate.Shared.Core.Dtos.WalletItem;

namespace CoreServicesTemplate.Shared.Core.Dtos.Wallet
{
    public class WalletApiDto : WalletApiBaseDto
    {
        public ICollection<WalletItemApiDto> WalletItems { get; set; }
    }
}