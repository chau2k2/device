namespace device.Response
{
    public class ResponseModel
    {
        public bool success { get; set; }
        public string message { get; set; }
        public ResponseModel(bool success, string message)
        {
            this.success = success;
            this.message = message;
        }
    }
}
