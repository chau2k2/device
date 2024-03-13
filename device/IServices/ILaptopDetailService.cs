using device.Entity;
using device.Response;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface ILaptopDetailService
    {
        Task<TPaging<LaptopDetailResponse>> GetAll(int page, int pageSize);
        Task<ActionResult<LaptopDetail>> GetById(int id);
        Task<ActionResult<LaptopDetail>> Update(int id, LaptopDetailResponse UpLD);
        Task<ActionResult<LaptopDetail>> Create(LaptopDetailResponse CrLD);
        Task<ActionResult<LaptopDetail>> Delete(int id);
    }
}
