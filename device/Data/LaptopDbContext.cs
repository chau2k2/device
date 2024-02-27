using device.Models;
using Microsoft.EntityFrameworkCore;

namespace device.Data
{
    public class LaptopDbContext :DbContext
    {
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
          
        }
        
        public DbSet<Laptop> laptops { get; set; }
        public DbSet<LaptopDetail> laptopsDetail { get; set; }
        public DbSet<Producer> producers { get; set; }
        public DbSet<Ram> ram { get; set; } 
        public DbSet<MonitorM> monitors { get; set; }
        public DbSet<Vga> vgas { get; set; }
        public DbSet<Storage> storages { get; set; }
        public DbSet<Invoice> invoices { get; set; }
        public DbSet<InvoiceDetail> InvoicesDetail { get;set; }

    }
}
