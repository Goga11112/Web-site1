using Geocoding.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_site1.Application.Services;
using Web_site1.Domain.Entities;
using Web_site1.Domain.Services;
using Web_site1.Infrastructure.Data;

namespace Web_site1.Presentation.Controllers
{

    public class WarehouseController : Controller
    {
        private readonly IWarehouseService _warehouseService;
        private readonly AppDbContext _context;

        public WarehouseController(IWarehouseService warehouseService, AppDbContext context)
        {
            _warehouseService = warehouseService;
            _context = context;
           
        }

        // Пример использования в методе действия
        public async Task<IActionResult> Index_w(string search)
        {
            IEnumerable<Warehouse> warehouses = await _warehouseService.GetAllWarehousesAsync();
            //  Фильтрация  
            if (!string.IsNullOrEmpty(search))
            {
                warehouses = warehouses.Where(p => p.Name.ToLower().Contains(search.ToLower()));
            }
           

            return View(warehouses);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_w(Warehouse warehouse)
        {

            if (!string.IsNullOrEmpty(warehouse.Name) && !string.IsNullOrEmpty(warehouse.Address) && !string.IsNullOrEmpty(warehouse.Longitude) && !string.IsNullOrEmpty(warehouse.Latitude)) //  <---  упрощенная проверка
            {
                await _warehouseService.CreateWarehouseAsync(warehouse);
                Console.WriteLine("Склад добавлен в базу данных");
                return RedirectToAction(nameof(Index_w));
            }
            else
            {
                // Добавляем ошибки в ModelState, если имя или адрес пусты
                if (string.IsNullOrEmpty(warehouse.Name))
                    ModelState.AddModelError(nameof(warehouse.Name), "Имя склада обязательно");
                if (string.IsNullOrEmpty(warehouse.Address))
                    ModelState.AddModelError(nameof(warehouse.Address), "Адрес склада обязателен");
                if (string.IsNullOrEmpty(warehouse.Latitude))
                    ModelState.AddModelError(nameof(warehouse.Latitude), "Долгота обязателена(указать через запятую)");
                if (string.IsNullOrEmpty(warehouse.Longitude))
                    ModelState.AddModelError(nameof(warehouse.Longitude), "Широта обязателена(указать через запятую)");
            }
            return View(warehouse); // <--- Возвращаем представление с моделью и ошибками
        }

        public async Task<IActionResult> Edit_w(int id)
        {
            // Получите склад по id из базы данных
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id);

            if (warehouse == null)
            {
                return NotFound(); // Или другая обработка, если склад не найден
            }

            return View(warehouse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_w(int id, Warehouse warehouse)
        {
                if (id != warehouse.Id)
                {
                    return NotFound();
                }
            if (!string.IsNullOrEmpty(warehouse.Name) && !string.IsNullOrEmpty(warehouse.Address) && !string.IsNullOrEmpty(warehouse.Longitude) && !string.IsNullOrEmpty(warehouse.Latitude)) //  <---  упрощенная проверка
            {
                try
                {
                    Console.WriteLine("Попал в идит");
                    await _warehouseService.UpdateWarehouseAsync(warehouse.Id, warehouse);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Обработка конкурентного доступа, если необходимо
                    throw;
                }
            }
            else
            {
                // Добавляем ошибки в ModelState, если имя или адрес пусты
                if (string.IsNullOrEmpty(warehouse.Name))
                    ModelState.AddModelError(nameof(warehouse.Name), "Имя склада обязательно");
                if (string.IsNullOrEmpty(warehouse.Address))
                    ModelState.AddModelError(nameof(warehouse.Address), "Адрес склада обязателен");
                if (string.IsNullOrEmpty(warehouse.Latitude))
                    ModelState.AddModelError(nameof(warehouse.Latitude), "Долгота обязателена(указать через запятую)");
                if (string.IsNullOrEmpty(warehouse.Longitude))
                    ModelState.AddModelError(nameof(warehouse.Longitude), "Широта обязателена(указать через запятую)");
            }
            var warehouses = await _warehouseService.GetWarehouseByIdAsync(id);

            return View(warehouses);
        }
            


        public async Task<IActionResult> Delete_w(int id)
        {
            // Получите склад по id из базы данных
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id);

            if (warehouse == null)
            {
                return NotFound(); // Или другая обработка, если склад не найден
            }

            return View(warehouse);
        }

        [HttpPost, ActionName("Delete_w")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Получите склад по id из базы данных
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id);

            if (warehouse != null)
            {
                await _warehouseService.DeleteWarehouseAsync(id);
            }
            return RedirectToAction("Index_w");
        }

        public async Task<IActionResult> Details_w(int id)
        {
            var warehouse = await _context.Warehouses
                .Include(w => w.ProductWarehouses)
                    .ThenInclude(pw => pw.Product) // Загрузите продукты через ProductWarehouse
                .FirstOrDefaultAsync(w => w.Id == id);

            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

       


    }
}