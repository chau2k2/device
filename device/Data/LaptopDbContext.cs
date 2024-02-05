using device.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace device.Data
{
    public class LaptopDbContext :DbContext
    {
        private readonly LaptopDbContext dbContext;
        private readonly IConfiguration Configuration;
        public LaptopDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("DeviceDB"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //rang buoc giua cac bang
            // n-n laptop - producers
            modelBuilder.Entity<Laptop>()
                .HasMany(p => p.Producers)
                .WithMany(p => p.Laptops)
                .UsingEntity(
                    "LaptopProducer",
                    l => l.HasOne(typeof(Producer)).WithMany().HasForeignKey("ProducerId").HasPrincipalKey(nameof(Producer.Id)).OnDelete(DeleteBehavior.Restrict),
                    p => p.HasOne(typeof(Laptop)).WithMany().HasForeignKey("LaptopId").HasPrincipalKey(nameof(Laptop.Id)).OnDelete(DeleteBehavior.Restrict),
                    j => j.HasKey("LaptopId","ProducerId"));
            //1-n
            //Laptop - laptopDetail
            modelBuilder.Entity<Laptop>()
                .HasMany(l => l.LaptopDetails)
                .WithOne(l => l.Laptops)
                .HasForeignKey(l => l.idLaptop)
                .OnDelete(DeleteBehavior.Restrict);
            // Ram - LaptopDetail
            modelBuilder.Entity<Ram>()
                .HasMany(r => r.LaptopDetail)
                .WithOne(r => r.Rams)
                .HasForeignKey(r => r.IdRam)
                .OnDelete(DeleteBehavior.Restrict);
            // Monitor - LaptopDetail
            modelBuilder.Entity<MonitorM>()
                .HasMany(m => m.LaptopDetail)
                .WithOne(m => m.Monitor)
                .HasForeignKey(m => m.IdMonitor)
                .OnDelete(DeleteBehavior.Restrict);
            // vga - LaptopDetail
            modelBuilder.Entity<Vga>()
                .HasMany(v => v.laptopDetail)
                .WithOne(v => v.Vga)
                .HasForeignKey(v => v.IdVga)
                .OnDelete(DeleteBehavior.Restrict);
            // 1-1 
            //LaptopDetail - kho hang
            modelBuilder.Entity<LaptopDetail>()
                .HasOne(l => l.khoHang)
                .WithOne(l => l.laptopDetail)
                .HasForeignKey<KhoHang>(l => l.idDetail)
                .OnDelete(DeleteBehavior.Restrict);
        }
        
        public DbSet<Laptop> laptops { get; set; }
        public DbSet<LaptopDetail> laptopsDetail { get; set; }
        public DbSet<Producer> producers { get; set; }
        public DbSet<Ram> ram { get; set; } 
        public DbSet<MonitorM> monitors { get; set; }
        public DbSet<Vga> vgas { get; set; }
        public DbSet<KhoHang> khoHangs { get; set; }

    }
}
