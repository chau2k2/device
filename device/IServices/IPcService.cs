using device.Entity;
using device.ModelResponse;
using device.Models;
using device.Response;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface IPcService
    {
        Task<TPaging<PcResponse>> GetAll ( int page, int pageSize );
        Task<ActionResult<BaseResponse<PrivateComputer>>> FindPc ( string name );
        Task<ActionResult<BaseResponse<PcResponse>>> Create(PrivateComputerModel model);
        Task<ActionResult<BaseResponse<PrivateComputer>>> Update(int id, PrivateComputerModel model);
        Task<ActionResult<BaseResponse<PrivateComputer>>> Delete (string name);
    }
}
