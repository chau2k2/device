using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using NuGet.Common;

namespace device.Response
{
    public class BaseResponse <T> 
    {
        public bool Success { get; set; }
        public string? Message { get; set; } 
        public ErrorCode? ErrorCode { get; set; }
        public T? Data { get; set; }
    }
    public enum ErrorCode
    {
        None,
        Error,
        NotFound
    }
}
