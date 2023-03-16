﻿using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Repositories
{
    public class WalletItemEfRepository : EfRepository<WalletItem>, IWalletItemRepository
    {
        private DbSet<WalletItem> WalletItems { get; }
        public WalletItemEfRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            WalletItems = appDbContext.Set<WalletItem>();
        }
        public IEnumerable<WalletItem> ReadWalletItemsByOwnerGuidAsync(Guid ownerGuid)
        {
            var walletItems = WalletItems.Where(og => og.ExtWallet.OwnerGuid == ownerGuid);
            return walletItems;
        }
    }
}
