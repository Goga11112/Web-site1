using System.ComponentModel.DataAnnotations;

namespace Web_site1.Domain.Entities
{
    public class Shirt : Product
    {
        [Display(Name = "Размер продукта")]
        [Required(ErrorMessage = "Введите размер")]
        [StringLength(3, ErrorMessage = "Более 3 символов")]
        public string Size { get; set; }
    }
}
