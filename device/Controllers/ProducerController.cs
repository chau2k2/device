﻿using device.IServices;
using device.Models;
using device.Views.Producer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            var result =  await _service.GetById(id);
            return Ok(result);
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
