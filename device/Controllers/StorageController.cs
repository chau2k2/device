using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        //private readonly LaptopDbContext _context;
        //private readonly P<Storage> _service;
        //private readonly StorageValidate _StorageValidate;
        //public StorageController(ILogger<StorageController> logger, IAllService<Storage> service, LaptopDbContext context)
        //{
        //    _context = context;
        //    _service = service;
        //    _StorageValidate = new StorageValidate();
        //}
        //[HttpGet("GetAll")]
        //public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        //{
        //    var result = await _service.GetAll(page, pageSize);
        //    return Ok(result);
        //}
        //[HttpPut]
        //public async Task<IActionResult> Update(int id, [FromBody] UpdateStorage USt)
        //{
        //    Storage storage = new Storage()
        //    {
        //        Id = id,
        //        idDetail = USt.idDetail,
        //        ImportNumber = USt.InserNumber,
        //        SoldNumber = USt.SaleNumber
        //    };

        //    try
        //    {
        //        var validate = _StorageValidate.Validate(storage);
        //        if (!validate.IsValid)
        //        {
        //            return BadRequest(validate.Errors);
        //        }
        //        var result = await _service.Update(id, storage);
        //        return Ok(result);
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
        //}
        //[HttpGet("Get/{id}")]
        //public async Task<IActionResult> FindById(int id)
        //{
        //    var result = await _service.GetById(id);
        //    if (result == null) { return NotFound(); }
        //    return Ok(result);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Add([FromBody] CreateStorage Cst)
        //{
        //    var maxId = await _context.storages.MaxAsync(s => (int?)s.Id) ?? 0;
        //    var nextId = maxId +1;

        //    Storage storage = new Storage()
        //    {
        //        Id = nextId,
        //        idDetail = Cst.idDetail,
        //        ImportNumber = Cst.InserNumber,
        //        SoldNumber = Cst.SaleNumber
        //    };
            
        //    try
        //    {
        //        var validate = _StorageValidate.Validate(storage);
        //        if (!validate.IsValid)
        //        {
        //            return BadRequest(validate.Errors);
        //        }
        //        var result = await _service.Add(storage);
        //        return Ok(result);
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
        //}
        //[HttpDelete]
        //public async Task<IActionResult> delete(int id)
        //{
        //    try
        //    {
        //        var del = await _service.Delete(id);
        //        if (del == null)
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
