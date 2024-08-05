using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web_site1.Domain.Entities
{
    public class Collector
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public User User { get; set; }

        // (Optional): Можно добавить Role property 
        public string Role { get; set; } = "Collector"; // "Collector" по умолчанию 

        public ICollection<OrderCollector> OrderCollectors { get; set; }
    }
}
