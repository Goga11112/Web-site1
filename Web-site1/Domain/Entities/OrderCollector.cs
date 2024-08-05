using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web_site1.Domain.Entities
{
    public class OrderCollector
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("OrderID")]
        public int OrderID { get; set; }
        public Order Order { get; set; }

        [ForeignKey("CollectorID")]
        public int CollectorID { get; set; }
        public Collector Collector { get; set; }
    }
}
