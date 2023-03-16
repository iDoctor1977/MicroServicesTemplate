using System;
using System.ComponentModel.DataAnnotations;

namespace CoreServicesTemplate.Shared.Core.Models
{
    public class CreateWalletApiDto
    {
        [Required]
        public Guid OwnerGuid { get; set; }

        [Required]
        public decimal? TradingAllowedBalance { get; set; }

        [Required]
        public decimal? OperationAllowedBalance { get; set; }

        [Required]
        public decimal? Balance { get; set; }
    }
}