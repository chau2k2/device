using device.Data;
using device.DTO.Producer;
using device.IServices;
using device.Models;
using device.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly ILogger<ProducerController> logger;
        private readonly IAllService<Producer> _service;
        private readonly ProducerValidate _producerValidate;
        private readonly LaptopDbContext _context;

        public ProducerController(ILogger<ProducerController> logger, IAllService<Producer> service,LaptopDbContext context)
        {
            this.logger = logger;
            _service = service;
            _context = context;
            _producerValidate = new ProducerValidate();
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            try
            {
                var result = await _service.GetAll(page,pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }      
        }

        [HttpPut]
        public async Task< IActionResult> Update(int id, [FromBody] UpdateProducer Upd)
        {
            Producer producer = new Producer()
            {
                Id = id,
                Name = Upd.Name,
                IsActive = Upd.IsActive
            };
            try
            {
                var validate = _producerValidate.Validate(producer);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                var result = await _service.Update(id, producer);
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
            var result =  await _service.GetById(id);
            if(result == null) { return NotFound(); }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProducer([FromBody]CreateProducer cpr)
        {
            int maxId = await _context.producers.MaxAsync(p => (int?)p.Id) ?? 0;
            int next = maxId + 1;

            Producer producer = new Producer()
            {
                Id = next,
                Name = cpr.Name,
                IsActive = cpr.IsActive
            };
            
            try
            {
                var validate = _producerValidate.Validate(producer);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                var result = await _service.Add(producer);
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
                var del = await _service.Delete(id);
                if(del == null)
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
