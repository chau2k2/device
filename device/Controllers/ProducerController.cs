using device.Data;
using device.DTO.Producer;
using device.IRepository;
using device.Models;
using device.Services;
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
        private readonly ProducerService _service;
        private readonly LaptopDbContext _context;
        private readonly IAllRepository<Producer> _allRepository;

        public ProducerController(ILogger<ProducerController> logger, LaptopDbContext context)
        {
            this.logger = logger;
            _service = new ProducerService(_allRepository);
            _context = context;
        }

        [HttpGet("get_all_producer")]
        public async Task<ActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            var result =  await _service.GetAll(page, pageSize);
            return Ok(result);
        }

        [HttpGet("get_producer_by_id")]
        public async Task<ActionResult> FindById(int id)
        {
            var result =  await _service.GetProducerById(id);
            return Ok(result);
        }
        [HttpPut]
        public async Task< ActionResult> Update(int id, [FromBody] UpdateProducer Upd)
        {
            Producer producer = new Producer()
            {
                Id = id,
                Name = Upd.Name,
                IsActive = Upd.IsActive
            };

            var result = _service.UpdateProducer(producer);
            return Ok(result);
        }

        [HttpPost("create_producer")]
        public async Task<ActionResult> CreateProducer([FromBody]CreateProducer cpr)
        {
            int maxId = await _context.producers.MaxAsync(p => (int?)p.Id) ?? 0;
            int next = maxId + 1;

            Producer producer = new Producer()
            {
                Id = next,
                Name = cpr.Name,
                IsActive = cpr.IsActive
            };

            var result = _service.CreateProducer(producer);
            return Ok(result);
        }

        //[HttpDelete]
        //public async Task<IActionResult> delete(int id)
        //{
        //    try
        //    {
        //        var del = await _service.Delete(id);
        //        if(del == null)
        //        {
        //            return NotFound();
        //        }
        //        return NoContent();
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        if (ex.InnerException is Npgsql.PostgresException postgresException)
        //        {
        //            string message = postgresException.MessageText;
        //            string constraintName = postgresException.ConstraintName;

        //            return BadRequest($"Error: {message}. Constraint: {constraintName}");
        //        }
        //        return StatusCode(500, "An error occurred while processing your request. Please try again later.");
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "An error occurred while processing your request. Please try again later.");
        //    }
        //}
    }
}
