using System.ComponentModel.DataAnnotations;

namespace Web_site1.Domain.Entities
{

    public class Dress : Product
    {
        [Display(Name = "Стиль продукта")]
        [Required(ErrorMessage = "Введите стиль")]
        public string Style { get; set; }
    }
}
