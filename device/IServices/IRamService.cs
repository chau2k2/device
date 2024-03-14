﻿using device.Entity;
using device.ModelResponse;
using device.Response;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface IRamService
    {
        Task<TPaging<Ram>> GetAll(int page, int pageSize);
        Task<ActionResult<BaseResponse<Ram>>> GetById(int id);
        Task<ActionResult<BaseResponse<Ram>>> Create(RamResponse CrR);
        Task<ActionResult<BaseResponse<Ram>>> Update(int id, RamResponse UpR);
        Task<ActionResult<BaseResponse<Ram>>> delete(int id);
    }
}