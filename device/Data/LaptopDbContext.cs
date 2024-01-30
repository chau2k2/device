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
                .WithMany(p => p.Laptops);
            //1-n
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
