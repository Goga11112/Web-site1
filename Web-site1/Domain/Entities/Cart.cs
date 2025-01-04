using Web_site1.Domain.Entities;

public class Cart
{
    public int Id { get; set; }
    public string UserId { get; set; } //  ID пользователя
    public List<CartItem> CartItems { get; set; } = new List<CartItem>();
}