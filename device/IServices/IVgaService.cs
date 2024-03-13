using device.Entity;
using device.ModelResponse;
using device.Response;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface IVgaService 
    {
        Task<ActionResult<BaseResponse<Vga>>> GetById(int id);
        Task<TPaging<VgaResponse>> GetAll(int page, int pageSize);
        Task<ActionResult<BaseResponse<Vga>>> Create(VgaResponse CrV);
        Task<ActionResult<BaseResponse<Vga>>> Update(int id, VgaResponse UpV);
        Task<ActionResult<BaseResponse<Vga>>> delete(int id);
    }
}
