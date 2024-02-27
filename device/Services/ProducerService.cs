using device.Data;
using device.IRepository;
using device.Models;
using device.Validation;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Drawing.Printing;

namespace device.Services
{
    public class ProducerService 
    {
        private readonly IAllRepository<Producer> _repos;
        private readonly ProducerValidate _validate;
        private readonly LaptopDbContext _context;
        public ProducerService(IAllRepository<Producer> repos, ProducerValidate validate, LaptopDbContext context)
        {
            _repos = repos;
            _validate = validate;
            _context = context;
        }
        public async Task<IEnumerable<Producer>> GetAll(int page = 1, int pageSize = 5)
        {
            try
            {
                var result = await _repos.GetAllAsync(page, pageSize);
                return result;
            }
            catch
            {
                return Enumerable.Empty<Producer>();
            }
        }
        public async Task<ActionResult<Producer>> GetProducerById( int id)
        {
            var result = await _repos.GetAsyncById(id);
            if (result == null)
            {
                return new NotFoundResult();
            }
            return result;
        }
        public async Task<ActionResult<Producer>> UpdateProducer (Producer producer)
        {
            try
            {
                var validate = _validate.Validate(producer);
                if (!validate.IsValid)
                {
                    return new ObjectResult(new { errors = validate.Errors });
                }
                var result = await _repos.UpdateOneAsyns(producer);
                return result;
            }
            catch (Exception ex)
            {
                return ;
            }
        }
        public async Task<ActionResult<Producer>> CreateProducer (Producer producer)
        {
            try
            {
                var validate = _validate.Validate(producer);
                if (!validate.IsValid)
                {
                    throw new ArgumentException();
                }
                    var result = await _repos.AddOneAsync(producer);
                return result;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is Npgsql.PostgresException postgresException)
                {
                    string message = postgresException.MessageText;
                    string constraintName = postgresException.ConstraintName;

                    return new ObjectResult($"Error: {message}. Constraint: {constraintName}");
                }
                return new ObjectResult($"Error: {"An error occurred while processing your request. Please try again later."}") { StatusCode = 500 };
            }
        }
        //public async Task<ActionResult<Producer>> DeleteProducer (int id)
        //{
        //    try
        //    {
        //        var findId = _repos.
        //        var del = await _repos.DeleteOneAsync(id);
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
