using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AssetMG.Models;

namespace AssetMG.Data
{
    public class AssetMGDbContext : DbContext
    {
        public AssetMGDbContext(DbContextOptions<AssetMGDbContext> options) : base(options) 
        { 
        }
        public DbSet<Assets> Assets { get; set; }
        public DbSet<Asset_Location> Locations { get; set; }
        public DbSet<Asset_Mvmt> Mvmt { get; set; }
        public DbSet<Asset_Mvmt_Type> MvmtTypes { get; set;}
        public DbSet<Asset_Operations> Operations { get; set; }
        public DbSet<Asset_Type> Types { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Users> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            modelBuilder.Entity<Assets>().ToTable("Assets");
            modelBuilder.Entity<Asset_Location>().ToTable("Asset_Location");
            modelBuilder.Entity<Asset_Mvmt>().ToTable("Asset_Mvmt");
            modelBuilder.Entity<Asset_Mvmt_Type>().ToTable("Asset_Mvmt_Type");
            modelBuilder.Entity<Asset_Operations>().ToTable("Asset_Operation");
            modelBuilder.Entity<Asset_Type>().ToTable("Asset_Type");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Assets>().HasKey(p => new {p.AssetTypeId, p.LocationId,p.CreateByUserId, p.DId});
            modelBuilder.Entity<Asset_Mvmt>().HasKey(p => new { p.AssetId, p.Uid, });
            modelBuilder.Entity<Asset_Operations>().HasKey(p => new { p.AssetId,p.Uid, });
            modelBuilder.Entity<Users>().HasKey(p => new {p.DId,});

        }

    }
}
