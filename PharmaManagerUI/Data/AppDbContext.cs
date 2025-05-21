using Microsoft.EntityFrameworkCore;
using PharmaManagerUI.Models;

namespace PharmaManagerUI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<RawMaterial> RawMaterials { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<ProductionOrder> ProductionOrders { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<QualityControl> QualityControls { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Logistic> Logistics { get; set; }
        public DbSet<PharmacyNetwork> PharmacyNetworks { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Drug>().ToTable("Drugs");
            modelBuilder.Entity<RawMaterial>().ToTable("Raw_Materials");
            modelBuilder.Entity<Equipment>().ToTable("Equipment");
            modelBuilder.Entity<ProductionOrder>().ToTable("Production_Orders");
            modelBuilder.Entity<Staff>().ToTable("Staff");
            modelBuilder.Entity<QualityControl>().ToTable("Quality_Control");
            modelBuilder.Entity<Warehouse>().ToTable("Warehouse");
            modelBuilder.Entity<Logistic>().ToTable("Logistics");
            modelBuilder.Entity<PharmacyNetwork>().ToTable("Pharmacy_Network");
            modelBuilder.Entity<Sale>().ToTable("Sales");
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}