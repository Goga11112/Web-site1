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
            Console.WriteLine(ModelState.IsValid);
            Console.WriteLine(product.Name); Console.WriteLine(product.Price); Console.WriteLine(product.Description); Console.WriteLine(product.ProductImageFile);
            try
            {
              Console.WriteLine("Product was saved");
            if (ModelState.IsValid && product.ProductImageFile != null)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,Size,Style, ProductImageFile, ProductImageUrl")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (product.ProductImageFile != null)
                {
                    string uniqueFileName = UploadedFile(product.ProductImageFile);
                    product.ProductImageUrl = "/images/uploads/" + uniqueFileName;
                }

                await _productService.UpdateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
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
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _productService.GetProductByIdAsync(id).Result != null;
        }

       
    }
}