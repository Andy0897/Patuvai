using System.ComponentModel.DataAnnotations;
using Patuvai.Models.Enums;

namespace Patuvai.ViewModels.RequestListing
{
    public class RequestListingFormViewModel
    {
        [Required(ErrorMessage = "Моля изберете тип транспорт")]
        [Display(Name = "Тип транспорт")]
        public TransportServiceType TransportServiceType { get; set; }

        [Required(ErrorMessage = "Моля въведете описание")]
        [StringLength(500, ErrorMessage = "Описанието трябва да бъде между {2} и {1} символа.", MinimumLength = 10)]
        [Display(Name = "Описание")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Моля въведете бюджет")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Бюджетът трябва да бъде положително число.")]
        [Display(Name = "Бюджет")]
        public double Budget { get; set; }

        [Required(ErrorMessage = "Моля изберете начална китна")]
        [Display(Name = "Начална китна")]
        public int FromCityId { get; set; }

        [Required(ErrorMessage = "Моля изберете целева китна")]
        [Display(Name = "Целева китна")]
        public int ToCityId { get; set; }

        [Required(ErrorMessage = "Моля въведете дата на пътуване")]
        [DataType(DataType.Date, ErrorMessage = "Моля въведете валидна дата.")]
        [Display(Name = "Дата на пътуване")]
        public DateOnly RequestDate { get; set; }
    }
}
