﻿using Microsoft.AspNetCore.Mvc;
using device.IServices;
using device.Models;

namespace device.Controllers
{
    [Route("api/laptop-detail")]
    [ApiController]
    public class LaptopDetailController : ControllerBase
    {
        private readonly ILaptopDetailService _service;
        private readonly ILogger<LaptopDetailController> _logger;

        public LaptopDetailController(ILogger<LaptopDetailController> logger, ILaptopDetailService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllLapDetail(int page = 1, int pageSize = 10)
        {
            return Ok ( await _service.GetAll(page,pageSize) );
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetOne(int id)
        {
            return Ok(await _service.GetById(id));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] LaptopDetailModel model)
        {
            return Ok (await _service.Create(model));
        }

        [HttpPost("update-lap-detail")]
        public async Task<IActionResult> Update(int id, [FromBody] LaptopDetailModel model)
        {
            return Ok (await _service.Update(id, model));
        }

        [HttpDelete("do-delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok ( await _service.Delete(id));
        }
    }
}
