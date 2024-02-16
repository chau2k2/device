using Microsoft.EntityFrameworkCore;
using Sale.Models;

namespace Sale.Data
{
    public class HoaDonDbContext : DbContext
    {
        public readonly IConfiguration Configuration;
        public HoaDonDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DeviceDB"));
        }
        public DbSet<HoaDon> HoaDones { get; set; }
        public DbSet<HoaDonDetail> HoaDonesDetail { get; set; }
    }
}
