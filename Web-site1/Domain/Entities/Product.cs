namespace Web_site1.Domain.Entities
{
    public abstract class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }

    public class Shirt : Product
    {
        public string Size { get; set; }
    }

    public class Dress : Product
    {
        public string Style { get; set; }
    }
}
