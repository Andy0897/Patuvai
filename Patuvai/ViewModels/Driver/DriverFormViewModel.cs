using System.ComponentModel.DataAnnotations;
using Patuvai.Validations;

namespace Patuvai.ViewModels.Driver
{
    public class DriverFormViewModel
    {

        [Required(ErrorMessage = "Представянето е задължителна.")]
        [StringLength(500, ErrorMessage = "Представянето не трябва да съдържа повече от 500 символа.")]
        [Display(Name = "Представяне")]
        public string Introduction { get; set; }

        [MustBeTrue(ErrorMessage = "Трябва да се съгласите с декларацията за шофьори.")]
        public bool AcceptDriverDeclaration { get; set; }

        //Трябва да се довърши
    }
}
