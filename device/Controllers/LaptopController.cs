﻿using device.IServices;
using device.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaptopController : ControllerBase
    {
        private readonly IAllService<Laptop> _service;

        public LaptopController(IAllService<Laptop> service)
        {
            _service = service;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            try
            {
                var result = await _service.GetAll(page, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                var result = await _service.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Laptop laptop)
        {
            var checkConstraint = await _service.CheckIdProducerOfProducer(laptop.Producer);
            if (checkConstraint)
            {
                return BadRequest("can not create this");
            }
            var result = await _service.Add(laptop);
            return Ok(result);
            
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Laptop laptop)
        {
            var result = await _service.Update(id, laptop);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            bool checkConstraint = await _service.CheckIdLaptop_LaptopDetail(id);
            if (checkConstraint)
            {
                return BadRequest("can not delete this");
            }
            var del = await _service.Delete(id);
            if (del == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
