using System.ComponentModel.DataAnnotations;

namespace Web_site1.Domain.Entities
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public ICollection<Stock> Stocks { get; set; }
    }
}
