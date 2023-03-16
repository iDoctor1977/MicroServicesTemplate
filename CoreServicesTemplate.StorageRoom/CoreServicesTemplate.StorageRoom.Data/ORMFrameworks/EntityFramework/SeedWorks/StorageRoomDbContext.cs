﻿using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.SeedWorks
{
    public class StorageRoomDbContext : DbContext, IAppDbContext
    {
        public DbSet<User> Users { get; set; }

        private readonly string _dbName;
        private readonly string _connectionStringName;

        public StorageRoomDbContext(DbContextOptions options) : base(options) { }

        public StorageRoomDbContext()
        {
            _dbName = "StorageRoomDb";
            _connectionStringName = CreateConnectionStringPath();
        }

        public StorageRoomDbContext(string dbName)
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
}
