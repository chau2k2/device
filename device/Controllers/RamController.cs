using device.Data;
using device.DTO.Ram;
using device.IServices;
using device.Models;
using device.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamController : ControllerBase
    {
        private readonly ILogger<RamController> logger;
        private readonly IAllService<Ram> _service;
        private readonly RamValidate _ramValidate;
        private readonly LaptopDbContext _context;

        public RamController(ILogger<RamController> logger, IAllService<Ram> service, LaptopDbContext context)
        {
            this.logger = logger;
            _service = service;
            _context = context;
            _ramValidate = new RamValidate();
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            var result = await _service.GetAll(page, pageSize);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRam udR)
        {
            Ram ram = new Ram()
            {
                Name = udR.Name,
                Price = udR.Price
            };
            try
            {
                var validate = _ramValidate.Validate(ram);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                var result = await _service.Update(id, ram);
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
            if (result == null) { return NotFound(); }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateRam CrR)
        {
            int maxId = await _context.ram.MaxAsync(r => (int?)r.Id) ?? 0;
            int nextId = maxId + 1;

            Ram ram = new Ram()
            {
                Id = nextId,
                Name = CrR.Name,
                Price = CrR.Price
            };
            
            try
            {
                var validate = _ramValidate.Validate(ram);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                var result = await _service.Add(ram);
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
