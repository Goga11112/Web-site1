using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_site1.Domain.Entities
{
    public class ProductWarehouse
    {
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }

        // Добавьте свойство навигации Product
        public Product Product { get; set; }

        // Добавьте свойство навигации Warehouse
        public Warehouse Warehouse { get; set; }
    }
}