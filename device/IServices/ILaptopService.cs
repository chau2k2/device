using device.Entity;
using device.Models;
using device.Response;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface ILaptopService 
    {
        Task<TPaging<LaptopResponse>> GetAllLaptop(int page, int pageSize);
        Task<ActionResult<BaseResponse<Laptop>>> GetLaptopById(int id);
        Task<ActionResult<BaseResponse<Laptop>>> Updatelaptop(int id, LaptopModel Upd);
        Task<ActionResult<BaseResponse<Laptop>>> CreateLaptop(LaptopModel crl);
        Task<ActionResult<BaseResponse<Laptop>>> DeleteLaptop(int id);
    }
}
