namespace device.Response
{
    public class BaseResponse <T> 
    {
        public bool success { get; set; }
        public string? message { get; set; } 
        public T? data { get; set; }
    }
}
