using Web_site1.Domain.Entities;

namespace Web_site1.Domain.Services
{
    public interface IShoppingCartService
    {
        Task AddToCart(int productId, int quantity);
        Task<ShoppingCart> GetCart();
        Task RemoveFromCart(int productId);
        Task UpdateQuantity(int productId, int quantity);
        Task ClearCart();
    }
}
