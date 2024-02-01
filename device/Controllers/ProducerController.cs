using device.IServices;
using device.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly ILogger<ProducerController> logger;
        private readonly IAllService<Producer> _service;
        public ProducerController(ILogger<ProducerController> logger, IAllService<Producer> service)
        {
            this.logger = logger;
            _service = service;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            var result = await _service.GetAll(page,pageSize);
            return Ok(result);
        }
        [HttpPut]
        public async Task< IActionResult> Update(int id, [FromBody] Producer producer)
        {
            var result = await _service.Update(id, producer);
            return Ok(result);
        }
        [HttpGet("Get")]
        public async Task<IActionResult> FindById(int id)
        {
            try
            {
                var result = await _service.GetById( id);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Producer producer)
        {
            var result = await _service.Add(producer);
            return Ok(result);
        }

        //public async Task<IActionResult> delete(int id)
        //{
        //    var result = _service.Delete<Producer>($"https://localhost:7121/api/producer/{id}",$"https://localhost:7121/api/producer/", id); 
        //    return Ok(result);
        //}
    }
}
