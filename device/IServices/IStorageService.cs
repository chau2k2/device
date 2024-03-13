using device.Entity;
using device.ModelResponse;
using device.Response;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface IStorageService
    {
        Task<TPaging<StorageResponse>> GetAll(int page, int pageSize);
        Task<ActionResult<BaseResponse<Storage>>> GetById(int id);
        Task<ActionResult<BaseResponse<Storage>>> Create(StorageResponse CrS);
        Task<ActionResult<BaseResponse<Storage>>> Update(int id, StorageResponse UpS);
        Task<ActionResult<BaseResponse<Storage>>> delete(int id);
    }
}
