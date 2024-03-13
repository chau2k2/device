using device.Data;
using device.IRepository;
using device.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.ModelResponse;
using device.IServices;
using device.Response;

namespace device.Services
{
    public class VgaService : IVgaService
    {
        private readonly IAllRepository<Vga> _repo;
        private readonly LaptopDbContext _context;

        public VgaService(IAllRepository<Vga> repo, LaptopDbContext context)
        {
            _repo = repo;
            _context = context;
        }
        public async Task<TPaging<VgaResponse>> GetAll(int page, int pageSize)
        {
            try
            {
                int totalCount = await _context.Set<Vga>().CountAsync();

                var result = await _context.Set<Vga>()!
                    .Include( v => v.laptopDetail)
                    .Where( v => v.IsDelete == false)
                    .Take(pageSize).Skip((page - 1) * pageSize)
                    .ToListAsync();

                List<VgaResponse> vgaResponses = new List<VgaResponse>();

                foreach (var vga in result)
                {
                    vgaResponses.Add(new VgaResponse() 
                    {
                        Id = vga.Id,
                        Name = vga.Name,
                        Price = vga.Price,
                        IsDelete = vga.IsDelete
                    });
                }

                return new TPaging<VgaResponse>()
                {
                    numberPage = page,
                    totalRecord = totalCount,
                    Data = vgaResponses
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<BaseResponse<Vga>>> GetById(int id)
        {
            try
            {
                var result = await _repo.GetAsyncById(id);

                if (result == null && result!.IsDelete == true)
                {
                    return new BaseResponse<Vga>
                    {
                        success = false,
                        message = "Not found!!!"
                    };
                }

                return new BaseResponse<Vga>
                {
                    success = true,
                    message = "Sucessfull!!!",
                    data = result
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<ActionResult<BaseResponse<Vga>>> Create(VgaResponse CrV)
        {
            try
            {
                int maxId = await _context.vgas.MaxAsync(r => (int?)r.Id) ?? 0;
                int nextId = maxId + 1;

                Vga vga = new Vga()
                {
                    Id = nextId,
                    Name = CrV.Name,
                    Price = CrV.Price
                };

                var result = await _repo.AddOneAsync(vga);

                return new BaseResponse<Vga>
                {
                    success = true,
                    message = "Sucessfull!!!",
                    data = result
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<BaseResponse<Vga>>> Update(int id, VgaResponse UpV)
        {
            var findId = await _context.ram.FindAsync(id);

            if (findId == null)
            {
                return new BaseResponse<Vga>
                {
                    success = false,
                    message = "Not found!!!"
                };
            }

            Vga vga = new Vga()
            {
                Id = id,
                Name = UpV.Name,
                Price = UpV.Price
            };

            try
            {
                var result = await _repo.UpdateOneAsyns(vga);

                return new BaseResponse<Vga> 
                { 
                    success = true, 
                    message = "Sucessfull!!!", 
                    data = result 
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<BaseResponse<Vga>>> delete(int id)
        {
            try
            {
                var vga = await _repo.GetAsyncById(id);

                if (vga == null)
                {
                    return new BaseResponse<Vga>
                    {
                        success = false,
                        message = "Not found!!!"
                    };
                }

                vga.IsDelete = true;

                var del = await _repo.UpdateOneAsyns(vga);

                return new BaseResponse<Vga>
                {
                    success = true,
                    message = "Sucessfull!!!",
                    data = del
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
