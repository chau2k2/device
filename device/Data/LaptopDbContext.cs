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
            // 1-n producers-laptop 
            modelBuilder.Entity<Producer>()
                .HasMany(p => p.Laptops)
                .WithOne(p => p.producer)
                .HasForeignKey(p => p.IdProducer)
                .OnDelete(DeleteBehavior.Restrict);
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
            //laptop - invoiceDetail
            //not done
            // 1-1 
            //LaptopDetail - storage
            modelBuilder.Entity<LaptopDetail>()
                .HasOne(l => l.storage)
                .WithOne(l => l.laptopDetail)
                .HasForeignKey<Storage>(l => l.idDetail)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
        
        public DbSet<Laptop> laptops { get; set; }
        public DbSet<LaptopDetail> laptopsDetail { get; set; }
        public DbSet<Producer> producers { get; set; }
        public DbSet<Laptop> ram { get; set; } 
        public DbSet<MonitorM> monitors { get; set; }
        public DbSet<Vga> vgas { get; set; }
        public DbSet<Storage> storages { get; set; }
        public DbSet<Invoice> invoices { get; set; }
        public DbSet<InvoiceDetail> InvoicesDetail { get;set; }

    }
}
