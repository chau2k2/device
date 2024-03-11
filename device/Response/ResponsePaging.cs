namespace device.Response
{
    public class ResponsePaging
    {
        public int numberPage { get; set; }
        public int totalPage { get; set; }
        public ResponsePaging(int numberPage, int totalPage)
        {
            this.numberPage = numberPage;
            this.totalPage = totalPage;
        }
    }
}
