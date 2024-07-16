using Microsoft.AspNetCore.Mvc;
using Web_site1.Domain.Services;
using Web_site1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace Web_site1.Presentation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public ProductController(IProductService productService, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
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
        public async Task<IActionResult> Create([Bind("Name,Price,Description,Size,Style, ProductImageFile, ProductImageUrl")] Product product)
        {
            if (ModelState.IsValid && product.ProductImageFile != null)
            {
                //  Сохраняем  картинку   на  диске:
                string uniqueFileName = UploadedFile(product.ProductImageFile);  //   Имя  файла 
                product.ProductImageUrl = "/images/uploads/" + uniqueFileName;  //   Путь   к   картинке   в  `wwwroot`

                // Сохраняем   товар  в   базу: 
                await _productService.CreateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        private string UploadedFile(IFormFile file)//загрузка файла
        {
            if (file == null || file.Length == 0) return null;

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images", "uploads");
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            file.CopyTo(new FileStream(filePath, FileMode.Create));
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