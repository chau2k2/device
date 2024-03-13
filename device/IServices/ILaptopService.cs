using device.Entity;
using device.Response;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface ILaptopService 
    {
        Task<TPaging<LaptopResponse>> GetAllLaptop(int page, int pageSize);
        Task<ActionResult<Laptop>> GetLaptopById(int id);
        Task<ActionResult<Laptop>> Updatelaptop(int id, LaptopResponse Upd);
        Task<ActionResult<Laptop>> CreateLaptop(LaptopResponse crl);
        Task<ActionResult<Laptop>> DeleteLaptop(int id);
    }
}
