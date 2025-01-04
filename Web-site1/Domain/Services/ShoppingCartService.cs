using System.Security.Claims;
using Web_site1.Domain.Entities;
using Web_site1.Domain.Services;
using Web_site1.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Web_site1.Domain.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductService _productService; // Для получения информации о товаре

        public ShoppingCartService(IHttpContextAccessor httpContextAccessor, IProductService productService,AppDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
            _dbContext = dbContext;
        }

        private string CartId => GetCartId();

        private string GetCartId()
        {
            // Используем сессию для хранения ID корзины
            return _httpContextAccessor.HttpContext.Session.GetString("CartId") ?? GenerateCartId();
        }

        private string GenerateCartId()
        {
            string cartId = Guid.NewGuid().ToString();
            _httpContextAccessor.HttpContext.Session.SetString("CartId", cartId);
            return cartId;
        }

        public async Task AddToCart(int productId, int quantity)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                // Обработка ошибки (например, логирование или исключение)
                return; // Или throw new Exception("Product not found");
            }

            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                // Обработка неавторизованного пользователя (например, использование сессии)
                return; // Или throw new Exception("User is not authenticated");
            }

            var dbCart = await _dbContext.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (dbCart == null)
            {
                dbCart = new Cart { UserId = userId };
                _dbContext.Carts.Add(dbCart);
            }

            var existingItem = dbCart.CartItems.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                _dbContext.CartItems.Update(existingItem); // Обновляем существующий элемент
            }
            else
            {
                dbCart.CartItems.Add(new CartItem { ProductId = productId, Quantity = quantity, Product = product, ProductName = product.Name });
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task SaveCart(ShoppingCart cart)
        {
            var userId = _httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return;

            var dbCart = await _dbContext.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (dbCart == null)
            {
                dbCart = new Cart { UserId = userId };
                _dbContext.Carts.Add(dbCart);
            }


            dbCart.CartItems.Clear(); // Очищаем существующие элементы

            // Добавляем или обновляем элементы из корзины
            foreach (var item in cart.Items)
            {
                dbCart.CartItems.Add(new CartItem
                {
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity,
                    Product = item.Product //  Связываем с продуктом
                });
            }



            _dbContext.Update(dbCart); // Обновляем корзину в базе данных
            await _dbContext.SaveChangesAsync();
        }



        private async Task<ShoppingCart> LoadCartFromDatabase()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var dbCart = await _dbContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (dbCart == null)
            {
                return new ShoppingCart();
            }

            return new ShoppingCart
            {
                Items = dbCart.CartItems.Select(ci => new CartItem { Product = ci.Product, Quantity = ci.Quantity }).ToList()
            };
        }


        // Пример синхронизации элементов корзины (внутри SaveCart)
        private void SyncCartItems(Cart dbCart, ShoppingCart cart)
        {
            foreach (var item in cart.Items)
            {
                var dbCartItem = dbCart.CartItems.FirstOrDefault(ci => ci.ProductId == item.Product.Id);
                if (dbCartItem == null)
                {
                    // Добавляем новый элемент
                    dbCart.CartItems.Add(new CartItem { ProductId = item.Product.Id, Quantity = item.Quantity });
                }
                else
                {
                    // Обновляем количество
                    dbCartItem.Quantity = item.Quantity;
                }
            }

            // Удаляем элементы, которых нет в корзине
            var toRemove = dbCart.CartItems.Where(ci => !cart.Items.Any(i => i.Product.Id == ci.ProductId)).ToList();
            _dbContext.CartItems.RemoveRange(toRemove);
        }

        public async Task<ShoppingCart> GetCart()
        {
            if (_httpContextAccessor.HttpContext == null) return new ShoppingCart();

            var userId = _httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return new ShoppingCart();


            return await LoadCartFromDatabase();
        }

        public async Task RemoveFromCart(int productId)
        {
            var cart = await GetCart();
            var itemToRemove = cart.Items.FirstOrDefault(i => i.Product.Id == productId);
            if (itemToRemove != null)
            {
                cart.Items.Remove(itemToRemove);
                SaveCart(cart); // не забудьте сохранить корзину
            }


        }

        public async Task UpdateQuantity(int productId, int quantity)
        {
            var cart = await GetCart();
            var itemToUpdate = cart.Items.FirstOrDefault(i => i.Product.Id == productId);

            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity = quantity;
                if (itemToUpdate.Quantity <= 0)
                {
                    await RemoveFromCart(productId); // удалить из козины, если quantity <= 0.

                }
                else
                {

                    SaveCart(cart); // не забудьте сохранить корзину
                }
            }

        }

        public async Task ClearCart()
        {
            var cart = await GetCart();
            cart.Items.Clear();
            SaveCart(cart); //  Не забудьте сохранить корзину

        }
    }
}
