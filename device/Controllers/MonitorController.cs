using device.IServices;
using device.Models;
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
        public MonitorController(ILogger<MonitorController> logger, IAllService<MonitorM> service)
        {
            this.logger = logger;
            _service = service;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            var result = await _service.GetAll(page, pageSize);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] MonitorM monitorM)
        {
            var result = await _service.Update(id, monitorM);
            return Ok(result);
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MonitorM monitorM)
        {
            try
            {
                var result = await _service.Add(monitorM);
                return Ok(result);
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
