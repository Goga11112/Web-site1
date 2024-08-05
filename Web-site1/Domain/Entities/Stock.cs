using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web_site1.Domain.Entities
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ProductID")]
        public int ProductID { get; set; }
        public Product Product { get; set; }

        [ForeignKey("WarehouseID")]
        public int WarehouseID { get; set; }
        public Warehouse Warehouse { get; set; }

        public int Quantity { get; set; }
    }
}
