using CoreServicesTemplate.StorageRoom.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.SeedWorks;

public class AppDbContext : DbContext, IAppDbContext
{
    private readonly string _dbName;
    private readonly string _connectionStringName;

    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<WalletItem> WalletItems { get; set; }

    public AppDbContext(DbContextOptions contextOptions) : base(contextOptions) { }

    public AppDbContext()
    {
        _dbName = "StorageRoomDb";
        _connectionStringName = CreateConnectionStringPath();
    }

    public AppDbContext(string dbName)
    {
        _dbName = dbName;
        _connectionStringName = CreateConnectionStringPath();
    }

    private string CreateConnectionStringPath()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        return $"Data Source={path}{Path.DirectorySeparatorChar}" + _dbName;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<User>().Property(i => i.Id).UseHiLo();
    }

    public void Commit()
    {
        SaveChanges();
    }

    public async Task CommitAsync()
    {
        await SaveChangesAsync();
    }
}