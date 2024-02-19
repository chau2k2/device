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
    public class StorageController : ControllerBase
    {
        private readonly ILogger<StorageController> logger;
        private readonly IAllService<Storage> _service;
        private readonly StorageValidate _khohangValidate;
        public StorageController(ILogger<StorageController> logger, IAllService<Storage> service)
        {
            this.logger = logger;
            _service = service;
            _khohangValidate = new StorageValidate();
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            var result = await _service.GetAll(page, pageSize);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] Storage khohang)
        {
            try
            {
                var validate = _khohangValidate.Validate(khohang);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                var result = await _service.Update(id, khohang);
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
        public async Task<IActionResult> Add([FromBody] Storage khohang)
        {
            try
            {
                var validate = _khohangValidate.Validate(khohang);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                var result = await _service.Add(khohang);
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
