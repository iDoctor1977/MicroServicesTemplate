using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Repositories;

public class WalletEfRepository : EfRepository<Wallet>, IWalletRepository
{
    private DbSet<Wallet> WalletEntity { get; }

    public WalletEfRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        WalletEntity = appDbContext.Set<Wallet>();
    }

    public async Task<IEnumerable<WalletItem>> ReadWalletItemsByOwnerGuidAsync(Guid ownerGuid)
    {
        var walletItems = (await WalletEntity.FirstAsync(og => og.OwnerGuid == ownerGuid)).ColWalletItems;

        return walletItems;
    }

    public async Task<Wallet?> ReadForOwnerGuidAsync(Guid ownerGuid)
    {
        var entity = await WalletEntity.FirstOrDefaultAsync(w => w.OwnerGuid == ownerGuid);

        return entity;
    }
}