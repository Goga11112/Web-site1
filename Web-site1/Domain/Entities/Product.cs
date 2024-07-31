using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Web_site1.Domain.Entities
{
    public class Product : IEnumerable<Product>
    {
        public int Id { get; set; }

        [Display(Name = "Название продукта")]
        [Required(ErrorMessage = "Введите название продукта")]
        public string Name { get; set; }

        [Display(Name = "Стоимость продукта")]
        [Required(ErrorMessage = "Введите цену")]
        [StringLength(6, ErrorMessage = "Более 6 символов")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")] // Укажите тип данных в базе данных
        public decimal Price { get; set; }

        [Display(Name = "Описание продукта")]
        [Required(ErrorMessage = "Введите описание")]
        public string Description { get; set; }


        [Display(Name = "Изображение продукта")]
        [Required(ErrorMessage = "Добавьте картинку")]
        public string ProductImageUrl { get; set; }

        [NotMapped]
        public IFormFile ProductImageFile { get; set; } //  Добавим свойство  для   файла картинки

        public IEnumerator<Product> GetEnumerator()
        {
            yield return this; //  Возвращаем  текущий  объект  Product
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
