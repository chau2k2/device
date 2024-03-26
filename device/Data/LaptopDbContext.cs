using device.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace device.Data
{
    public class LaptopDbContext :IdentityDbContext <User, Role, int>
    {
        private readonly IConfiguration Configuration;
        public LaptopDbContext(IConfiguration configuration, DbContextOptions option)
            :base(option)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("DeviceDB"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
        public DbSet<PrivateComputer> PrivateComputer { get; set; }
        public DbSet<User> User { get; set; }

    }
}
