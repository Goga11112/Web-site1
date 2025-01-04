using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web_site1.Domain.Services;

namespace Web_site1.Presentation.Controllers
{
    public class BasketController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ILogger<BasketController> _logger; 

        
        public BasketController(IShoppingCartService shoppingCartService, ILogger<BasketController> logger)
        {
            _shoppingCartService = shoppingCartService;
            _logger = logger; 
        }

        public async Task<IActionResult> Basket_View()
        {
            var cart = await _shoppingCartService.GetCart();
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            try
            {
                await _shoppingCartService.AddToCart(productId, quantity);
                return Json(new { success = true, message = "Товар успешно добавлен в корзину!" });
            }
            catch (Exception ex)
            {
                // Залогируйте исключение
                _logger.LogError(ex, "Error adding product to cart"); //  Если у вас есть ILogger
                Console.WriteLine(ex.Message); 
                // Верните ответ с ошибкой
                return StatusCode(500, new { success = false, message = "Ошибка при добавлении товара в корзину." }); // Или более подробное сообщение об ошибке
            }
        }
    }
}
