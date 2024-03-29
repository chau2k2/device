﻿using device.Entity;
using device.Models;
using device.Response;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface IInvoiceDetailService
    {
        Task<TPaging<InvoiceDetailResponse>> GetAllInvoiceDetail(int page, int pageSize);
        Task<ActionResult<BaseResponse<InvoiceDetail>>> CreateInvoiceDetail(InvoiceDetailModel model);
        Task<ActionResult<BaseResponse<InvoiceDetail>>> Update(int id, InvoiceDetailModel model);
        Task<ActionResult<BaseResponse<InvoiceDetail>>> Delete(int id);
        Task<ActionResult<BaseResponse<InvoiceDetail>>> GetById(int id);
    }
}
