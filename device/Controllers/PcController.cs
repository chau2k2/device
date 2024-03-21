using device.IServices;
using device.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/pc")]
    [ApiController]
    public class PcController : ControllerBase
    {
        private readonly IPcService _service;

        public PcController(IPcService service) 
        {
            _service = service;
        }
        [HttpGet("get-all-pc")]
        public async Task<IActionResult> GetAll (int page = 1, int pageSize = 5)
        {
            return Ok( await _service.GetAll(page, pageSize));
        }

        [HttpPost("create-pc")]
        public async Task<IActionResult> Create (PrivateComputerModel pcModel)
        {
            return Ok ( await _service.Create(pcModel));
        }
    }
}
