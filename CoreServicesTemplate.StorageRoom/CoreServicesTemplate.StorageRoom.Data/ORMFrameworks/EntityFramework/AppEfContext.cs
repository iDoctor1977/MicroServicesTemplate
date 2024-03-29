﻿using CoreServicesTemplate.Shared.Core.Interfaces.IData;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework;

public class AppEfContext : DbContext, IUnitOfWorkContext
{
    private readonly string _dbName;
    private readonly string _connectionStringName;

    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<WalletItem> WalletItems { get; set; }

    public AppEfContext(DbContextOptions contextOptions) : base(contextOptions) { }

    public AppEfContext()
    {
        _dbName = "StorageRoomDb";
        _connectionStringName = CreateConnectionStringPath();
    }

    public AppEfContext(string dbName)
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
        //modelBuilder.Entity<Wallet>().Property(i => i.Id).UseHiLo();
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