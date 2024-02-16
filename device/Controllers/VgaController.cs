﻿using device.IServices;
using device.Models;
using device.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VgaController : ControllerBase
    {
        private readonly ILogger<VgaController> logger;
        private readonly IAllService<Vga> _service;
        private readonly VgaValidate _vgaValidate;

        public VgaController(ILogger<VgaController> logger, IAllService<Vga> service)
        {
            this.logger = logger;
            _service = service;
            _vgaValidate = new VgaValidate();
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            var result = await _service.GetAll(page, pageSize);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] Vga vga)
        {
            try
            {
                var validate = _vgaValidate.Validate(vga);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                var result = await _service.Update(id, vga);
                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is Npgsql.PostgresException postgresException)
                {
                    string message = postgresException.MessageText;
                    string constraintName = postgresException.ConstraintName;

                    return BadRequest($"Error: {message}. Constraint: {constraintName}");
                }
                return StatusCode(500, "An error occurred while processing your request. Please try again later.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request. Please try again later.");
            }
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Vga vga)
        {
            try
            {
                var validate = _vgaValidate.Validate(vga);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                var result = await _service.Add(vga);
                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is Npgsql.PostgresException postgresException)
                {
                    string message = postgresException.MessageText;
                    string constraintName = postgresException.ConstraintName;

                    return BadRequest($"Error: {message}. Constraint: {constraintName}");
                }
                return StatusCode(500, "An error occurred while processing your request. Please try again later.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request. Please try again later.");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                return Ok(await _service.Delete(id));
            }
            catch (DbUpdateException ex) when (ex.InnerException is Npgsql.PostgresException postgresException)
            {
                string message = postgresException.MessageText;
                string constraintName = postgresException.ConstraintName;

                return BadRequest($"Error: {message}. Constraint: {constraintName}");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request. Please try again later.");
            }
        }   
    }
}