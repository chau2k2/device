using device.IRepository;
using device.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailController : ControllerBase
    {
        protected readonly IAllRepository<InvoiceDetail> _repo;
        public InvoiceDetailController (IAllRepository<InvoiceDetail> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            return Ok(await _repo.GetAllAsync(page, pageSize));
        }
    }
}
