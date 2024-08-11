using Microsoft.AspNetCore.Identity;

namespace Web_site1.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int PurchaseCount { get; set; }

        // Это свойство будет определять уровень пользователя на основе количества покупок
        public UserRank Rank { get; set; }

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
    }

    public enum UserRank
    {
        Bronze,
        Silver,
        Gold
    }
}
