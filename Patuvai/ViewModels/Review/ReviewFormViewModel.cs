using System.ComponentModel.DataAnnotations;

namespace Patuvai.ViewModels.Review
{
    public class ReviewFormViewModel
    {
        public int DriverId { get; set; }

        [Range(1, 5, ErrorMessage = "Рейтингът трябва да бъде между 1 и 5.")]
        public int Rating { get; set; }

        [Length(0, 500, ErrorMessage = "Коментарът не трябва да съдържа повече от 500 символа.")]
        public string Comment { get; set; } = string.Empty;
    }
}
