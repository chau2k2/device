using Microsoft.EntityFrameworkCore;

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
        
    }
}
