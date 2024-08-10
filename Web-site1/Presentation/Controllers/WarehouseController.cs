﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_site1.Application.Services;
using Web_site1.Domain.Entities;
using Web_site1.Domain.Services;

namespace Web_site1.Presentation.Controllers
{

    public class WarehouseController : Controller
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
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
            try
            {
                if ((warehouse.Name != null) && (warehouse.Address != null))
                {

                    // Сохраняем   склад  в   базу: 
                    await _warehouseService.CreateWarehouseAsync(warehouse);
                    Console.WriteLine("Склад добавлен в базу данных");
                    return RedirectToAction(nameof(Index));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(warehouse);
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

            if (ModelState.IsValid)
            {
                try
                {
                    await _warehouseService.UpdateWarehouseAsync(warehouse.Id,warehouse);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Обработка конкурентного доступа, если необходимо
                    throw;
                }
            }
            return View(warehouse);
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Получите склад по id из базы данных
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id);

            if (warehouse != null)
            {
                await _warehouseService.DeleteWarehouseAsync(id);
            }
            return RedirectToAction("Index");
        }
    }
}