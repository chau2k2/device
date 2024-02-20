using device.DTO.Monitor;
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
    public class MonitorController : ControllerBase
    {
        private readonly ILogger<MonitorController> logger;
        private readonly IAllService<MonitorM> _service;
        private readonly MonitorValidate _monitorValidate;
        public MonitorController(ILogger<MonitorController> logger, IAllService<MonitorM> service)
        {
            this.logger = logger;
            _service = service;
            _monitorValidate = new MonitorValidate();
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            var result = await _service.GetAll(page, pageSize);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMonitor UMn)
        {
            MonitorM monitor = new MonitorM()
            {
                Id = id,
                Name = UMn.Name,
                Price = UMn.Price
            };
            try
            {
                var validate = _monitorValidate.Validate(monitor);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                var result = await _service.Update(id,monitor);
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
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateMonitor CMn)
        {
            MonitorM monitor = new MonitorM()
            {
                Id = CMn.Id,
                Name = CMn.Name,
                Price= CMn.Price
            };
            try
            {
                var validate = _monitorValidate.Validate(monitor);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                var result = await _service.Add(monitor);
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
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                return Ok (await _service.Delete(id));
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
