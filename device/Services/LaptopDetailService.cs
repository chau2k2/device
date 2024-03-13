﻿using device.Data;
using device.IRepository;
using device.Entity;
using device.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.IServices;

namespace device.Services
{
    public class LaptopDetailService : ILaptopDetailService
    {
        private readonly ILogger<LaptopDetailService> _logger;
        private readonly IAllRepository<LaptopDetail> _repos;
        private readonly LaptopDbContext _context;

        public LaptopDetailService(IAllRepository<LaptopDetail> repos, ILogger<LaptopDetailService> logger, LaptopDbContext context)
        {
            this._logger = logger;
            _repos = repos;
            _context = context;
        }

        public async Task<TPaging<LaptopDetailResponse>> GetAll(int page, int pageSize)
        {
            try
            {
                int totalCount = await _context.Set<LaptopDetail>().CountAsync();
                int totalPage = (int)Math.Ceiling((double)totalCount / pageSize);

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
                return new TPaging<LaptopDetailResponse>
                {
                    numberPage = page,
                    totalRecord = totalCount,
                    Data = laptopDetailResponse
                };
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
        public async Task<ActionResult<LaptopDetail>> Update(int id, LaptopDetailResponse UpLD)
        {
            var findId = await _repos.GetAsyncById(id);
            if (findId == null)
            {
                throw new Exception("Not found LaptopDetail");
            }
            LaptopDetail laptopDetail = new LaptopDetail()
            {
                Id = id,
                Cpu = UpLD.Cpu,
                Seri = UpLD.Seri,
                VgaId = UpLD.VgaId,
                RamId = UpLD.RamId,
                HardDriver = UpLD.HardDriver,
                MonitorId = UpLD.MonitorId,
                Webcam = UpLD.Webcam,
                Weight = UpLD.Weight,
                Height = UpLD.Height,
                Width = UpLD.Width,
                Length = UpLD.Length,
                BatteryCapacity = UpLD.BatteryCapacity,
                LaptopId = UpLD.LaptopId
            };
            try
            {
                var result = await _repos.UpdateOneAsyns(laptopDetail);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<LaptopDetail>> Create(LaptopDetailResponse CrLD)
        {
            int maxId = await _context.laptopsDetail.MaxAsync(p => (int?)p.Id) ?? 0;
            int next = maxId + 1;

            LaptopDetail laptopDetail = new LaptopDetail()
            {
                Id = next,
                Cpu = CrLD.Cpu,
                Seri = CrLD.Seri,
                VgaId = CrLD.VgaId,
                RamId = CrLD.RamId,
                HardDriver = CrLD.HardDriver,
                MonitorId = CrLD.MonitorId,
                Webcam = CrLD.Webcam,
                Weight = CrLD.Weight,
                Height = CrLD.Height,
                Width = CrLD.Width,
                Length = CrLD.Length,
                BatteryCapacity = CrLD.BatteryCapacity,
                LaptopId = CrLD.LaptopId
            };

            try
            {
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
                    throw new Exception("Not found Laptop Detail");
                }
                laptopDetail.IsDelete = true;
                var del = await _repos.DeleteOneAsync(laptopDetail);
                return del;
            }
            catch (Exception)
            {
                throw new Exception("Can't delete this laptop detail");
            }
        }
    }
}
