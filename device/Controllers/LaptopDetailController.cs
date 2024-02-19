using device.Data;
using device.DTO.LaptopDetail;
using device.IServices;
using device.Models;
using device.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
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
        public async Task<IActionResult> Create([FromBody] CreateLaptopDetail CLD)
        {
            LaptopDetail detail = new LaptopDetail()
            {
                Id = CLD.Id,
                Cpu = CLD.Cpu,
                Seri = CLD.Seri,
                IdVga = CLD.IdVga,
                IdRam = CLD.IdRam,
                HardDriver = CLD.HardDriver,
                IdMonitor = CLD.IdMonitor,
                Webcam = CLD.Webcam,
                Weight = CLD.Weight,
                Height = CLD.Height,
                Width = CLD.Width,
                Length = CLD.Length,
                BatteryCapacity = CLD.BatteryCapacity,
                idLaptop = CLD.idLaptop
            };
            try
            {
                var validate = _detailValidate.Validate(detail);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                var result = await _service.Add(detail);
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLaptopDetail UDL)
        {
            LaptopDetail detail = new LaptopDetail()
            {
                Id = id,
                Cpu = UDL.Cpu,
                Seri = UDL.Seri,
                IdVga = UDL.IdVga,
                IdRam = UDL.IdRam,
                HardDriver = UDL.HardDriver,
                IdMonitor = UDL.IdMonitor,
                Webcam = UDL.Webcam,
                Weight = UDL.Weight,
                Height = UDL.Height,
                Width = UDL.Width,
                Length = UDL.Length,
                BatteryCapacity = UDL.BatteryCapacity,
                idLaptop = UDL.idLaptop
            };
            try
            {
                var validate = _detailValidate.Validate(detail);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                var result = await _service.Update(id, detail);
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
