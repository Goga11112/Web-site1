namespace Web_site1.Domain.Entities
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
    }
}
