using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Web_site1.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public int PurchaseCount { get; set; } = 0; // Значение по умолчанию

        [Required]
        public UserRank Rank { get; set; } = UserRank.Bronze; // Значение по умолчанию

        public string Role { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public string AvatarUrl { get; set; }

        public string Bio { get; set; }

        public void UpdateUserRank()
        {
            Rank = PurchaseCount switch
            {
                >= 5 => UserRank.Gold,
                >= 3 => UserRank.Silver,
                _ => UserRank.Bronze,
            };
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}".Trim();
        }

        // Добавленный метод для удобного обновления ранга и количества покупок 
        public async Task UpdateUser(string userId, int productsAdded, UserManager<ApplicationUser> userManager)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.PurchaseCount += productsAdded; //Обновляем количество покупок
                user.UpdateUserRank();
                await userManager.UpdateAsync(user);
            }
        }

    }


    public enum UserRank
    {
        Bronze,
        Silver,
        Gold
    }
}