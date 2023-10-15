using System.Collections.Generic;
using CoreServicesTemplate.Shared.Core.Models.WalletItem;

namespace CoreServicesTemplate.Shared.Core.Models.Wallet
{
    public class ResponseWalletApiDto : WalletApiDtoBase
    {
        public ICollection<MarketItemApiDto> WalletItems { get; set; }
    }
}