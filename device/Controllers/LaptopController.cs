using device.DTO.Laptop;
using device.IServices;
using device.Models;
using device.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaptopController : ControllerBase
    {
        private readonly IAllService<Laptop> _service;
        private readonly LaptopValidate _laptopValidate;
        public LaptopController(IAllService<Laptop> service)
        {
            _service = service;
            _laptopValidate = new LaptopValidate();
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
        public async Task<IActionResult> Create([FromBody] CreateLaptop CrL)
        {
            Laptop laptop = new Laptop()
            {
                Id = CrL.Id,
                Name = CrL.Name,
                IdProducer = CrL.IdProducer,
                SoldPrice = CrL.SoldPrice,
                CostPrice = CrL.CostPrice
            };
            try
            {
                var validate = _laptopValidate.Validate(laptop);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                var result = await _service.Add(laptop);
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
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLaptop UdL)
        {
            Laptop laptop = new Laptop()
            {
                Id = id,
                Name = UdL.Name,
                IdProducer = UdL.IdProducer,
                CostPrice = UdL.CostPrice,
                SoldPrice = UdL.SoldPrice
            };
            try
            {
                var validate = _laptopValidate.Validate(laptop);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                var result = await _service.Update(id,laptop);
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
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var del = await _service.Delete(id);
                if (del == null)
                {
                    return NotFound();
                }
                return NoContent();
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
    }
}
