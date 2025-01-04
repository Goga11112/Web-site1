using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_site1.Domain.Entities;

namespace Web_site1.Presentation.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var model = new UserProfileViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                BirthDate = user.BirthDate,
                AvatarUrl = user.AvatarUrl,
                Bio = user.Bio,
                Rank = user.Rank,
                PurchaseCount = user.PurchaseCount,
                Role = user.Role
            };
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(model);
        }

        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var model = new UserProfileViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                BirthDate = user.BirthDate,
                AvatarUrl = user.AvatarUrl,
                Bio = user.Bio,
                Rank = user.Rank,
                PurchaseCount = user.PurchaseCount,
                Role = user.Role
            };
            return View(model); // Передаем модель в представление
        }

        [HttpPost]
        public async Task<IActionResult> SaveEditedProfile(UserProfileViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.BirthDate = model.BirthDate;
            user.AvatarUrl = model.AvatarUrl;
            user.Bio = model.Bio;

            user.UpdateUserRank(); // Обновление ранга пользователя

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Не удалось обновить профиль.");
                return View(model);
            }

            TempData["SuccessMessage"] = "Профиль успешно изменен"; // Сохраняем сообщение в TempData
            return RedirectToAction("Profile");
        }
    }

}
