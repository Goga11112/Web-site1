using Microsoft.AspNetCore.Identity;

namespace Web_site1.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int PurchaseCount { get; set; }

        // Это свойство будет определять уровень пользователя на основе количества покупок
        public UserLevel Level => GetUserLevel();

        private UserLevel GetUserLevel()
        {
            if (PurchaseCount >= 100)
                return UserLevel.Gold;
            else if (PurchaseCount >= 50)
                return UserLevel.Silver;
            else
                return UserLevel.Bronze;
        }
    }

    public enum UserLevel
    {
        Bronze,
        Silver,
        Gold
    }
}
