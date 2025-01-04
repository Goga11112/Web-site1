namespace Web_site1.Domain.Entities
{
    public class CartItem
    {
        public int Id { get; set; } //  ID  позиции в корзине (необязательно, но полезно)
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }


        public Product Product { get; set; } // Ссылка на товар
        public Cart Cart { get; set; }
    }
}
