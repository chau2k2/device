﻿using Microsoft.AspNetCore.Mvc;
using device.ModelResponse;
using device.IServices;
using device.Models;

namespace device.Controllers
{
    [Route("api/ram")]
    [ApiController]
    public class RamController : ControllerBase
    {
        private readonly IRamService _service;
        private readonly ILogger<RamController> _logger;

        public RamController(IRamService service, ILogger<RamController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            return Ok(await _service.GetAll(page, pageSize));
        }

        [HttpPut("do-update")]
        public async Task<IActionResult> Update(int id, [FromBody] RamModel udR)
        {
            return Ok (await _service.Update(id, udR));
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> FindById(int id)
        {
            return Ok (await _service.GetById(id));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] RamModel CrR)
        {
            return Ok (await _service.Create(CrR)); 
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> delete(int id)
        {
            return Ok (await _service.delete(id));
        }
    }
}
