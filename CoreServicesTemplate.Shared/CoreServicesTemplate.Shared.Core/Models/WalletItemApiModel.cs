using System;
using System.ComponentModel.DataAnnotations;

namespace CoreServicesTemplate.Shared.Core.Models;

public class WalletItemApiModel
{
    [Required] public Guid GuId { get; set; }

    [Required]
    public decimal? Amount { get; set; }

    [Required]
    public decimal? BuyPrice { get; set; }

    [Required]
    public DateTime? BuyDate { get; set; }

    [Required]
    public int Quantity { get; set; }

    public DateTime? DateUpdated { get; set; }
}