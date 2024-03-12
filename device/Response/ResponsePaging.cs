namespace device.Response
{
    public class TPaging<T>
    {
        public int numberPage { get; set; }
        public int totalRecord { get; set; }
        public IEnumerable<T> Data { get; set;}
    }
}
