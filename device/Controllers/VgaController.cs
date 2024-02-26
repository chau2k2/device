using device.DTO.Vga;
using device.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VgaController : ControllerBase
    {
        private readonly string _connectString = "Host=localhost; Database=DEVICE; Username=postgres; Password=123456789";

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Vga>>> SelectAllVga()
        //{
        //    List<Vga> Vgas = new List<Vga>();
        //    using (NpgsqlConnection conn = new NpgsqlConnection(_connectString))
        //    {
        //        await conn.OpenAsync();
        //        using(NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM FC_GetAllVga();",conn)) 
        //        {
        //            using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    Vga vga = new Vga()
        //                    {
        //                        Id = reader.GetInt32(0),
        //                        Name = reader.GetString(1),
        //                        Price = reader.GetDouble(2)
        //                    };
        //                    Vgas.Add( vga );
        //                }
        //            }
        //        }
        //    }
        //    return Vgas;
        //}

        [HttpPost]
        public async Task<IActionResult> CreateVga(CreateVga vgs)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand("insertVga", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("namevga", vgs.Name);
                        cmd.Parameters.AddWithValue("price", vgs.Price);

                        await cmd.ExecuteNonQueryAsync();
                    }

                    conn.Close();

                }
                return Ok("create vga successfull.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVga(int id, UpdateVga Uvga)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand("updateVga", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("idvga", id);
                        cmd.Parameters.AddWithValue("namevga", Uvga.Name);
                        cmd.Parameters.AddWithValue("price", Uvga.Price);

                        await cmd.ExecuteNonQueryAsync();
                    }

                    conn.Close();

                }
                return Ok("update vga successfull.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteVga(int id)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand("deleteVga", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("idvga", id);

                        await cmd.ExecuteNonQueryAsync();
                    }

                    conn.Close();

                }
                return Ok("delete vga successfull.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
