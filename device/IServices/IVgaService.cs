using device.Entity;
using device.ModelResponse;
using device.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace device.IServices
{
    public interface IVgaService 
    {
        Task<ActionResult<Vga>> GetById(int id);
        Task<IEnumerable<Vga>> GetAll(int page, int pageSize);
        Task<ActionResult<Vga>> Create(VgaResponse CrV);
        Task<ActionResult<Vga>> Update(int id, VgaResponse UpV);
        Task<ActionResult<Vga>> delete(int id);
    }
}
