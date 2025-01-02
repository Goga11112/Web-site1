using System.ComponentModel.DataAnnotations;

namespace Web_site1.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен быть не менее {2} символов.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Роль")]
        public string Role { get; set; }

        public List<string> AvailableRoles { get; set; } = new List<string> { "Покупатель", "Продавец", "Администратор" };

        [Required]
        [Display(Name = "Имя")]
        [StringLength(50, ErrorMessage = "Имя не может быть длиннее {1} символов.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        [StringLength(50, ErrorMessage = "Фамилия не может быть длиннее {1} символов.")]
        public string LastName { get; set; }

        [Display(Name = "Адрес")]
        [StringLength(200, ErrorMessage = "Адрес не может быть длиннее {1} символов.")]
        public string Address { get; set; }

        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Аватар (URL)")]
        [DataType(DataType.Url)]
        public string AvatarUrl { get; set; }

        [Display(Name = "Биография")]
        [StringLength(500, ErrorMessage = "Биография не может быть длиннее {1} символов.")]
        public string Bio { get; set; }
    }
}
