using Microsoft.AspNetCore.Mvc;
using Web_site1.Domain.Services;
using Web_site1.Domain.Entities;
using Microsoft.AspNetCore.Hosting; //  Для  `IWebHostEnvironment`
using Microsoft.AspNetCore.Http;  //   Для   `IFormFile` 
using Azure.Messaging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web_site1.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_site1.Domain.Repositories;
using MyClothingApp.Domain.Services;

namespace Web_site1.Presentation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _env;
        private readonly IWarehouseService _warehouseService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IProductWarehouseService _productWarehouseService; //  Добавить  репозиторий  ProductWarehouse

        public ProductController(IProductService productService, IWebHostEnvironment env, IProductWarehouseService productWarehouseService, IWarehouseService warehouseService, UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _productService = productService;
            _env = env;
            _warehouseService = warehouseService; 
            _userManager = userManager;
            _context = context;
            _productWarehouseService = productWarehouseService;
        }


        public async Task<IActionResult> Index(string search)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                ViewData["Email"] = user.Email;
                // Передача ранга пользователя в представление
                ViewBag.UserRank = user?.Rank ?? UserRank.Bronze;
            }
            else
            {
                ViewBag.UserRank = UserRank.Bronze; // Например, если пользователь не залогинен, назначаем минимальный ранг
            }

            var products = await _productService.GetAllProductsAsync();

            //  Фильтрация  
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.ToLower().Contains(search.ToLower()));
            }

            return View(products);
        }

        public IActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Warehouses = await _warehouseService.GetAllWarehousesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                if ((product.ProductImageFile != null) && (product.Name != null) && (product.Price != 0))
                {
                    //  Сохраняем  картинку   на  диске:
                    string uniqueFileName = UploadedFile(product.ProductImageFile);  //   Имя  файла 
                    product.ProductImageUrl = "/images/uploads/" + uniqueFileName;  //   Путь   к   картинке   в  `wwwroot`

                    //  Сохраняем   товар  в   базу: 
                    await _productService.CreateProductAsync(product);

                    //  Создаем  ProductWarehouse  после  сохранения  продукта
                    var productWarehouse = new ProductWarehouse
                    {
                        ProductId = product.Id,
                        WarehouseId = product.WarehouseId,
                        Quantity = product.Quantity
                    };

                    //  Сохраняем  ProductWarehouse  в  базе  данных
                    await _productWarehouseService.CreateProductWarehouseAsync(productWarehouse);

                    Console.WriteLine("Успешно добавлен в базу данных");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(product);
        }

        private string UploadedFile(IFormFile file) //Сохранение файла на диск
        {
            string uploadsFolder = Path.Combine(_env.WebRootPath, "images", "uploads");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            Console.WriteLine("Файл успешно добавлен в диск");
            return uniqueFileName;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            var warehouses = await _warehouseService.GetAllWarehousesAsync();
            ViewBag.Warehouses = warehouses.Select(w => new { w.Id, w.Name }).ToList();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            // Проверяем, заполнены ли обязательные поля
            if (string.IsNullOrEmpty(product.Name) || product.Price <= 0 || string.IsNullOrEmpty(product.Description))
            {
                Console.WriteLine("Валидация не пройдена");
                // Добавьте логику отображения сообщений об ошибке валидации (например, добавив
                // ошибки в ModelState и вызвав View(product)
                return View(product); // Возвращаем View с ошибками валидации
            }



            // Валидация пройдена 
            Console.WriteLine("Валидация пройдена");
            if (product.ProductImageFile != null)
            {
                // Сохранение нового файла в библиотеку, не забывайте о обработке исключений
                string uniqueFileName = UploadedFile(product.ProductImageFile);
                product.ProductImageUrl = "/images/uploads/" + uniqueFileName;
                Console.WriteLine("Файл добавлен в библиотеку");
            }

            // Обновление продукта в базе данных
            try
            {
                await _productService.UpdateProductAsync(product);
                Console.WriteLine("Обновили в базе");
                return RedirectToAction(nameof(Index)); // Перенаправление на список товаров
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении продукта: {ex.Message}");
                // Добавьте логику обработки исключений: отобразите сообщение об ошибке
                // пользователю или перенаправьте его на страницу с сообщением об ошибке. 
                return View(product); //  Возвращаем View с информацией о продукте,
                                      // чтобы пользователь мог попытаться обновить его снова.
            }
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Удаление товара из базы данных
            try
            {
                var product = await _productService.GetProductByIdAsync(id);

                if (product != null)
                {
                    // Получение пути к файлу с изображением 
                    string filePath = Path.Combine(_env.WebRootPath, "images", "uploads", product.ProductImageUrl.Replace("/images/uploads/", ""));

                    // Удаление товара
                    await _productService.DeleteProductAsync(id);

                    // Проверка на существование файла и удаление
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                        Console.WriteLine($"Файл '{product.ProductImageUrl}' был удален.");
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении продукта: {ex.Message}");
                return RedirectToAction(nameof(Index)); // Или, return View(product)
            }

        }

        [HttpPost]
        public async Task<IActionResult> Buy(int productId)
        {
            // Получаем текущего пользователя
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // Если пользователь не авторизован, перенаправляем его на страницу логина
                return RedirectToAction("Login", "Account");
            }
            // Логика для добавления товара в заказ
            // Здесь можно добавить товар в корзину или создать новый заказ

            // Увеличиваем счетчик покупок
            user.PurchaseCount++;
            user.UpdateUserRank();
            // Сохраняем изменения в базе данных
            await _userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();

            // Перенаправляем пользователя на страницу с товарами или на страницу подтверждения заказа
            return RedirectToAction("Index", "Product");
        }


    }
}