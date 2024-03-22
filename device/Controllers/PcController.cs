using device.IServices;
using device.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.OpenApi.Writers;

namespace device.Controllers
{
    [Route("api/pc")]
    [ApiController]
    public class PcController : ControllerBase
    {
        private readonly IPcService _service;
        private readonly ILogger<PcController> _logger;

        public PcController(IPcService service, ILogger<PcController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("get-all-pc")]
        public async Task<IActionResult> GetAll (int page = 1, int pageSize = 5)
        {
            return Ok( await _service.GetAll(page, pageSize));
        }

        [HttpPost("create-pc")]
        public async Task<IActionResult> Create (PrivateComputerModel model)
        {
            return Ok ( await _service.Create(model));
        }

        [HttpPost("update-pc")]
        public async Task<IActionResult> Update (int id, PrivateComputerModel model)
        {
            return Ok (await _service.Update(id, model));
        }
        [HttpDelete("delete-pc")]
        public async Task<IActionResult> Delete (string name)
        {
            return Ok (await _service.Delete(name));
        }
    }
}
