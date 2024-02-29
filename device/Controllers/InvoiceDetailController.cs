﻿using device.Data;
using device.DTO.HDonDetail;
using device.IRepository;
using device.Models;
using device.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailController : ControllerBase
    {
        //protected readonly IAllRepository<InvoiceDetail> _repo;
        //private readonly LaptopDbContext _dbcontext;
        //private readonly InvoiceDetailValidation _validate;
        //public InvoiceDetailController(IAllRepository<InvoiceDetail> repo, LaptopDbContext dbContext)
        //{
        //    _dbcontext = dbContext;
        //    _repo = repo;
        //    _validate = new InvoiceDetailValidation(dbContext);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        //{
        //    return Ok(await _repo.GetAllAsync(page, pageSize));
        //}
        ////[HttpGet("GetByInvoiceNum/Notdone")]
        ////public async Task<IActionResult> FindByInvoiceNum(string InvoiceNum)
        ////{
        ////    return Ok();
        ////}
        //[HttpPost]
        //public async Task<IActionResult> CreateInvoiceDetail(CreateInvoiceDetail CID)
        //{
        //    int maxId = await _dbcontext.InvoicesDetail.MaxAsync(d => (int?)d.Id) ?? 0;
        //    int nextId = maxId + 1;

        //    InvoiceDetail detail = new InvoiceDetail()
        //    {
        //        Id = nextId,

        //        Quantity = CID.Quantity
        //    };

        //    _dbcontext.invoices.FirstOrDefault(i => i.Id == CID.IdInvoice);

        //    var laptop = _dbcontext.laptops.FirstOrDefault(l => l.Id == CID.IdLaptop);
        //    if (laptop != null) { detail.Price = laptop.SoldPrice; }

        //    try
        //    {
        //        var val = _validate.Validate(detail);
        //        if (!val.IsValid) { return BadRequest(val.Errors); }
        //        var result = await _repo.AddOneAsync(detail); return Ok(result);
        //    }
        //    catch
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Create Invoice Detail not successfull!!!");
        //    }
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateInvoice(int id, UpdateInvoiceDetail UID)
        //{
        //    InvoiceDetail detail = new InvoiceDetail()
        //    {
        //        Id = id,
        //        IdInvoice = UID.IdInvoice,
        //        Quantity = UID.Quantity
        //    };

        //    if (detail.IdLaptop != UID.IdLaptop)
        //    {
        //        var laptop = _dbcontext.laptops.FirstOrDefault(l => l.Id == detail.IdLaptop);
        //        if (laptop != null) { detail.Price = laptop.SoldPrice; }
        //    }
        //    else
        //    {
        //        detail.Price = detail.Price;
        //    }

        //    try
        //    {
        //        var val = _validate.Validate(detail);
        //        if (!val.IsValid) { return BadRequest(val.Errors); }
        //        var result = await _repo.UpdateOneAsyns(detail); return Ok(result);
        //    }
        //    catch
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Update Invoice Detail not successfull!!!");
        //    }
        //}

        //[HttpDelete]
        //public async Task<IActionResult> DeleteInvoiceDetail(int id)
        //{
        //    try
        //    {
        //        var findId = await _repo.GetAsyncById(id);
        //        if (findId == null) { return NotFound(); }
        //        return Ok(await _repo.DeleteOneAsync(findId));
        //    }
        //    catch (DbUpdateException ex) when (ex.InnerException is Npgsql.PostgresException postgresException)
        //    {
        //        string message = postgresException.MessageText;
        //        string constraintName = postgresException.ConstraintName;

        //        return BadRequest($"Error: {message}. Constraint: {constraintName}");
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "An error occurred while processing your request. Please try again later.");
        //    }
        //}
    }
}
