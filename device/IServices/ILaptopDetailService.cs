using device.Entity;
using device.Models;
using device.Response;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface ILaptopDetailService
    {
        Task<TPaging<LaptopDetailResponse>> GetAll(int page, int pageSize);
        Task<ActionResult<BaseResponse<LaptopDetail>>> GetById(int id);
        Task<ActionResult<BaseResponse<LaptopDetail>>> Update(int id, LaptopDetailModel UpLD);
        Task<ActionResult<BaseResponse<LaptopDetail>>> Create(LaptopDetailModel CrLD);
        Task<ActionResult<BaseResponse<LaptopDetail>>> Delete(int id);
    }
}
