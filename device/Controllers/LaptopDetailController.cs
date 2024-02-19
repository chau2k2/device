using device.Data;
using device.IServices;
using device.Models;
using device.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaptopDetailController : ControllerBase
    {
        private readonly IAllService<LaptopDetail> _service;
        private readonly LaptopDetailValidate _detailValidate;
        public LaptopDetailController(IAllService<LaptopDetail> service, LaptopDbContext dbContext)
        {
            _service = service;
            _detailValidate = new LaptopDetailValidate(dbContext);
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
        public async Task<IActionResult> Create([FromBody] LaptopDetail laptopDetail)
        {
            try
            {
                var validate = _detailValidate.Validate(laptopDetail);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                var result = await _service.Add(laptopDetail);
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
        [HttpPost("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LaptopDetail laptopDetail)
        {
            try
            {
                var validate = _detailValidate.Validate(laptopDetail);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                var result = await _service.Update(id, laptopDetail);
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
        public async Task<IActionResult> Delete(int id)
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
