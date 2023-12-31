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
        public DbSet<Asset_Type> Asset_Type { get; set; }
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
            modelBuilder.Entity<Assets>(
                entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.HasOne<Asset_Type>(d => d.AssetType)
                    .WithMany().HasForeignKey(d => d.AssetTypeId);
                    entity.HasOne<Users>(d => d.CreatedByUser)
                    .WithMany().HasForeignKey(d => d.CreateByUserId);
                    entity.HasOne<Department>(d => d.Department)
                    .WithMany().HasForeignKey(d => d.DId);
                    entity.HasOne<Asset_Location>(d => d.Location)
                    .WithMany().HasForeignKey(d => d.LocationId);
                });
            modelBuilder.Entity<Asset_Mvmt>(
                entity =>
                {
                    entity.HasKey(e => e.MId);

                    entity.HasOne<Assets>(d => d.Assets)
                    .WithMany().HasForeignKey(d => d.AssetId);
                   
                    entity.HasOne<Asset_Mvmt_Type>(d => d.Type)
                    .WithMany().HasForeignKey(d => d.AMId);

                    entity.HasOne<Users>(d => d.Users)
                    .WithMany().HasForeignKey(d => d.Uid);

                });
            modelBuilder.Entity<Asset_Operations>(
                entity =>
                {
                    entity.HasKey(e => e.Operations_Id);
                    entity.HasOne<Assets>(d => d.Assets)
                    .WithMany().HasForeignKey(d => d.AssetId);

                    entity.HasOne<Users>(d => d.Users)
                    .WithMany().HasForeignKey(d => d.Uid);
                });
            modelBuilder.Entity<Users>(
                entity =>
                {
                    entity.HasKey(e => e.Uid);
                    entity.HasOne<Department>(d => d.Department)
                    .WithMany().HasForeignKey(d => d.DId);
                });

        }

    }
}
 