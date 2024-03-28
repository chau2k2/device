namespace device.Response
{
    public class TPaging<T>
    {
        public int NumberPage { get; set; }
        public int TotalRecord { get; set; }
        public ErrorCode Error { get; set; }
        public string? Message { get; set; }
        public IEnumerable<T> Data { get; set;}
    }
}
