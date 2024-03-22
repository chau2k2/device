using device.Entity;
using device.Models;
using device.Response;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface ILaptopService 
    {
        Task<TPaging<LaptopResponse>> GetAllLaptop(int page, int pageSize);
        Task<ActionResult<BaseResponse<LaptopResponse>>> GetLaptopById(int id);
        Task<ActionResult<BaseResponse<Laptop>>> Updatelaptop(int id, LaptopModel model);
        Task<ActionResult<BaseResponse<Laptop>>> CreateLaptop(LaptopModel model);
        Task<ActionResult<BaseResponse<Laptop>>> DeleteLaptop(int id);
        Task<ActionResult<BaseResponse<IEnumerable<LaptopResponse>>>> SearchLaptop(string? name, string? producer, decimal? firstPrice, decimal? endPrice);
    }
}
