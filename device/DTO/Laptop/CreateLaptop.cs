namespace device.DTO.Laptop
{
    public class CreateLaptop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdProducer { get; set; }
        public double CostPrice { get; set; }
        public double SoldPrice { get; set; }
    }
}
