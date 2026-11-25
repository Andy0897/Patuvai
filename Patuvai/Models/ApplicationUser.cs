using System.ComponentModel.DataAnnotations;
using Patuvai.Models.Enums;
namespace Patuvai.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        [Display(Name = "Име")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Имейл")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Display(Name = "Телефон")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Роля")]
        public UserRole Role { get; set; } = UserRole.User;
    }
}
