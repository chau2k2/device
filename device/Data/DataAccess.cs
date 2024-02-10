namespace device.Data
{
    public class DataAccess
    {
        private readonly LaptopDbContext _context;
        public DataAccess (IConfiguration configuration)
        {
            _context = new LaptopDbContext(configuration);
        }
        
    }
}
