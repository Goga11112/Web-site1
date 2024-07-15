using System.ComponentModel.DataAnnotations;

namespace Web_site1.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Название продукта")]
        [Required(ErrorMessage = "Введите название продукта")]
        public string Name { get; set; }

        [Display(Name = "Стоимость продукта")]
        [Required(ErrorMessage = "Введите цену")]
        [StringLength(3, ErrorMessage = "Более 3 символов")]
        public decimal Price { get; set; }

        [Display(Name = "Описание продукта")]
        [Required(ErrorMessage = "Введите описание")]
        public string Description { get; set; }
    }
}
