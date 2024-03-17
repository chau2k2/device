using device.Entity;
using device.ModelResponse;
using device.Models;
using device.Response;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface IVgaService 
    {
        Task<ActionResult<BaseResponse<Vga>>> GetById(int id);
        Task<TPaging<VgaResponse>> GetAll(int page, int pageSize);
        Task<ActionResult<BaseResponse<Vga>>> Create(VgaModel CrV);
        Task<ActionResult<BaseResponse<Vga>>> Update(int id, VgaModel UpV);
        Task<ActionResult<BaseResponse<Vga>>> delete(int id);
    }
}
