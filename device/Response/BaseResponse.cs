namespace device.Response
{
    public class BaseResponse <T> 
    {
        public bool Success { get; set; }
        public string? Message { get; set; } 
        public string? Error { get; set; }
        public T? Data { get; set; }
    }
}
