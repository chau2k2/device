using System.ComponentModel.DataAnnotations.Schema;

namespace device.DTO.Storage
{
    public class CreateStorage
    {
        public int Id { get; set; }
        public int idDetail { get; set; }
        public int SaleNumber { get; set; }
        public int InserNumber { get; set; }
    }
}
