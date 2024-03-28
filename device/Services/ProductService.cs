using device.Data;
using device.Entity;
using device.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace device.Services
{
    public class ProductService
    {
        private readonly LaptopDbContext _context;

        public ProductService(LaptopDbContext context) 
        {
            _context = context;
        }
        public async Task< decimal> ProductTypePrice (decimal priceEntity, int productId, EProductType productType)
        {
            switch (productType)
            {
                case EProductType.Laptop:

                    var laptop = await _context.laptops.FirstOrDefaultAsync(l => l.Id == productId);

                    if (laptop != null)
                    {
                        priceEntity = laptop!.SoldPrice;
                    }

                    break;
                case EProductType.PrivateComputer:

                    var pc = await _context.PrivateComputer.FirstOrDefaultAsync(p => p.Id == productId);

                    if (pc != null)
                    {
                        priceEntity = pc!.SoldPrice;
                    }
                    break;

                case EProductType.Ram:
                    var ram = await _context.ram.FirstOrDefaultAsync(r => r.Id == productId);

                    if (ram != null)
                    {
                        priceEntity = ram!.Price;
                    }
                    break;

                case EProductType.Monitor:
                    var monitor = await _context.monitors.FirstOrDefaultAsync(m => m.Id == productId);

                    if (monitor != null)
                    {
                        priceEntity = monitor!.Price;
                    }
                    break;

                case EProductType.Vga:
                    var vga = await _context.vgas.FirstOrDefaultAsync(v => v.Id == productId);

                    if (vga != null)
                    {
                        priceEntity = vga!.Price;
                    }

                    break;    
            }
            return priceEntity;
        }
    }
}
