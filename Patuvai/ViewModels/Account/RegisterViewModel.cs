using System.ComponentModel.DataAnnotations;
using Patuvai.Validations;

namespace Patuvai.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Името е задължително.")]
        [Display(Name = "Име")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Фамилията е задължителна.")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Имейлът е задължителен.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл адрес.")]
        [Display(Name = "Имейл")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Телефонът е задължителен.")]
        [Phone(ErrorMessage = "Невалиден телефонен номер.")]
        [Display(Name = "Телефонен номер")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Паролата е задължителна.")]
        [MinLength(6, ErrorMessage = "Паролата трябва да бъде поне 6 символа.")]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Потвърждението на паролата е задължително.")]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
        [DataType(DataType.Password)]
        [Display(Name = "Потвърди парола")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [MustBeTrue(ErrorMessage = "Трябва да се съгласите с условията.")]
        public bool AcceptTerms { get; set; }

        [MustBeTrue(ErrorMessage = "Трябва да се съгласите с политиката за поверителност.")]
        public bool AcceptPrivacyPolicy { get; set; }
    }
}
