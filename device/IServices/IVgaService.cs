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
        Task<ActionResult<BaseResponse<Vga>>> Create(VgaModel model);
        Task<ActionResult<BaseResponse<Vga>>> Update(int id, VgaModel model);
        Task<ActionResult<BaseResponse<Vga>>> Delete(int id);
    }
}
