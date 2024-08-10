using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_site1.Domain.Entities
{
    public class Warehouse : IEnumerable<Warehouse>
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Display(Name = "Название склада")]
        [Required(ErrorMessage = "Введите название склада")]
        public string Name { get; set; }

        [Display(Name = "Адрес склада")]
        [Required(ErrorMessage = "Введите адрес склада")]
        public string Address { get; set; }
        // Добавьте другие свойства склада по необходимости, например:
        // public string PhoneNumber { get; set; }
        // public bool IsActive { get; set; }

        // Ссылка на товары, хранящиеся на складе (один-ко-многим)
        public List<Product> Products { get; set; }

        public IEnumerator<Warehouse> GetEnumerator()
        {
            yield return this; //  Возвращаем  текущий  объект  Warehouse
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
