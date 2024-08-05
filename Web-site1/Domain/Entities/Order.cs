using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web_site1.Domain.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public User User { get; set; }

        [ForeignKey("ProductID")]
        public int ProductID { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }

        public OrderCollector OrderCollector { get; set; }
    }
}
