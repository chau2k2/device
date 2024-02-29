using device.Data;
using device.DTO.LaptopDetail;
using device.IRepository;
using device.Models;
using device.Response;
using device.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace device.Services
{
    public class LaptopDetailService
    {
        private readonly ILogger<LaptopDetailService> _logger;
        private readonly IAllRepository<LaptopDetail> _repos;
        private readonly LaptopDetailValidate _validate;
        private readonly LaptopDbContext _context;

        public LaptopDetailService(IAllRepository<LaptopDetail> repos, ILogger<LaptopDetailService> logger, LaptopDbContext context)
        {
            this._logger = logger;
            _repos = repos;
            _validate = new LaptopDetailValidate();
            _context = context;
        }

        public async Task<IEnumerable<LaptopDetailResponse>> GetAll(int page, int pageSize)
        {
            try
            {
                var result = await _context.Set<LaptopDetail>()!
                    .Include(l => l.Laptops)
                    .Include(r => r.Rams)
                    .Include(m => m.Monitor)
                    .Include(v => v.Vga)
                    .Include(s => s.Storage)
                    .Take(page).Skip((page - 1) * pageSize)
                    .ToListAsync();

                List<LaptopDetailResponse> laptopDetailResponse = new List<LaptopDetailResponse>();

                foreach (var laptopDetail in result)
                {
                    laptopDetailResponse.Add(new LaptopDetailResponse()
                    {
                        Id = laptopDetail.Id,
                        Cpu = laptopDetail.Cpu,
                        Seri = laptopDetail.Seri,
                        VgaName = laptopDetail.Vga.Name,
                        RamName = laptopDetail.Rams.Name,
                        HardDriver = laptopDetail.HardDriver,
                        MonitorName = laptopDetail.Monitor.Name,
                        Webcam = laptopDetail.Webcam,
                        Weight = laptopDetail.Weight,
                        Height = laptopDetail.Height,
                        Width = laptopDetail.Width,
                        Length = laptopDetail.Length,
                        BatteryCapacity = laptopDetail.BatteryCapacity,
                        LaptopName = laptopDetail.Laptops.Name,
                        IsDelete = laptopDetail.IsDelete
                    }) ;
                }
                return laptopDetailResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<LaptopDetail>> GetById(int id)
        {
            var result = await _repos.GetAsyncById(id);
            if (result == null)
            {
                return new NotFoundResult();
            }
            return result;
        }
        public async Task<ActionResult<LaptopDetail>> Update(int id, UpdateLaptopDetail UpLD)
        {
            var findId = await _repos.GetAsyncById(id);
            if (findId == null)
            {
                throw new Exception("not found LaptopDetail");
            }
            LaptopDetail laptopDetail = new LaptopDetail()
            {
                Id = id,
                Cpu = UpLD.Cpu,
                Seri = UpLD.Seri,
                VgaId = UpLD.IdVga,
                RamId = UpLD.IdRam,
                HardDriver = UpLD.HardDriver,
                MonitorId = UpLD.IdMonitor,
                Webcam = UpLD.Webcam,
                Weight = UpLD.Weight,
                Height = UpLD.Height,
                Width = UpLD.Width,
                Length = UpLD.Length,
                BatteryCapacity = UpLD.BatteryCapacity,
                LaptopId = UpLD.idLaptop
            };
            try
            {
                var validate = _validate.Validate(laptopDetail);
                if (!validate.IsValid)
                {
                    throw new Exception(string.Join(", ", validate.Errors));
                }

                var result = await _repos.UpdateOneAsyns(laptopDetail);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<LaptopDetail>> Create(CreateLaptopDetail CrLD)
        {
            int maxId = await _context.laptopsDetail.MaxAsync(p => (int?)p.Id) ?? 0;
            int next = maxId + 1;

            LaptopDetail laptopDetail = new LaptopDetail()
            {
                Id = next,
                Cpu = CrLD.Cpu,
                Seri = CrLD.Seri,
                VgaId = CrLD.IdVga,
                RamId = CrLD.IdRam,
                HardDriver = CrLD.HardDriver,
                MonitorId = CrLD.IdMonitor,
                Webcam = CrLD.Webcam,
                Weight = CrLD.Weight,
                Height = CrLD.Height,
                Width = CrLD.Width,
                Length = CrLD.Length,
                BatteryCapacity = CrLD.BatteryCapacity,
                LaptopId = CrLD.idLaptop
            };

            try
            {
                var validate = _validate.Validate(laptopDetail);
                if (!validate.IsValid)
                {
                    throw new Exception(string.Join(", ", validate.Errors));
                }
                var result = await _repos.AddOneAsync(laptopDetail);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<LaptopDetail>> Delete(int id)
        {
            try
            {
                var laptopDetail = await _repos.GetAsyncById(id);
                if (laptopDetail == null)
                {
                    throw new Exception("not found Laptop Detail");
                }
                laptopDetail.IsDelete = true;
                var del = await _repos.DeleteOneAsync(laptopDetail);
                return del;
            }
            catch (Exception)
            {
                throw new Exception("cant delete this laptop detail");
            }
        }
    }
}
