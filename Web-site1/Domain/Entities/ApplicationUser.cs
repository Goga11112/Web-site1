using Microsoft.AspNetCore.Identity;

namespace Web_site1.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public UserLevel Level { get; set; }  //  (изменение  на  вашу  модель) 
        public int PurchaseCount { get; set; }
    }

    //   Добавление   Enum
    public enum UserLevel
    {
        Bronze,
        Silver,
        Gold
    }
}