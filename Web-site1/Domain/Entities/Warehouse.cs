namespace Web_site1.Domain.Entities
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        // Добавьте другие свойства склада по необходимости, например:
        // public string PhoneNumber { get; set; }
        // public bool IsActive { get; set; }

        // Ссылка на товары, хранящиеся на складе (один-ко-многим)
        public List<Product> Products { get; set; }
    }
}
