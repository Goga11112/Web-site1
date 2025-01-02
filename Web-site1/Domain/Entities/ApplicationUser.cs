using Microsoft.AspNetCore.Identity;

namespace Web_site1.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // Количество покупок
        public int PurchaseCount { get; set; }

        // Ранг пользователя (на основе количества покупок)
        public UserRank Rank { get; set; }

        // Роль пользователя
        public string Role { get; set; }

        // Имя пользователя
        public string FirstName { get; set; }

        // Фамилия пользователя
        public string LastName { get; set; }

        // Адрес пользователя
        public string Address { get; set; }

        // Дата рождения
        public DateTime? BirthDate { get; set; }

        // Аватар пользователя (ссылка на изображение)
        public string AvatarUrl { get; set; }

        // Биография или описание пользователя
        public string Bio { get; set; }

        // Метод для обновления ранга пользователя на основе количества покупок
        public void UpdateUserRank()
        {
            if (PurchaseCount >= 5)
            {
                Rank = UserRank.Gold;
            }
            else if (PurchaseCount >= 3)
            {
                Rank = UserRank.Silver;
            }
            else
            {
                Rank = UserRank.Bronze;
            }
        }

        // Метод для получения полного имени пользователя
        public string GetFullName()
        {
            return $"{FirstName} {LastName}".Trim();
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
