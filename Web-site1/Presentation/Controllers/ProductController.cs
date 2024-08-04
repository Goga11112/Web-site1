using Microsoft.AspNetCore.Mvc;
using Web_site1.Domain.Services;
using Web_site1.Domain.Entities;
using Microsoft.AspNetCore.Hosting; //  Для  `IWebHostEnvironment`
using Microsoft.AspNetCore.Http;  //   Для   `IFormFile` 
using Azure.Messaging;

namespace Web_site1.Presentation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _env;

        public ProductController(IProductService productService, IWebHostEnvironment env)
        {
            _productService = productService;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        public async Task<IActionResult> Details(int? id)
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

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product) 
        {
            try
            {
                if ((product.ProductImageFile != null)&&(product.Name!=null)&&(product.Price != 0))
                {
                        //  Сохраняем  картинку   на  диске:
                        string uniqueFileName = UploadedFile(product.ProductImageFile);  //   Имя  файла 
                        product.ProductImageUrl = "/images/uploads/" + uniqueFileName;  //   Путь   к   картинке   в  `wwwroot`
                        Console.WriteLine("Спрошло сохранение в базу");

                        // Сохраняем   товар  в   базу: 
                        await _productService.CreateProductAsync(product);
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
                await _productService.DeleteProductAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Обработка ошибок удаления 
                Console.WriteLine($"Ошибка при удалении продукта: {ex.Message}");
                return RedirectToAction(nameof(Index)); // Или, return View(product)
            }
        }



    }
}