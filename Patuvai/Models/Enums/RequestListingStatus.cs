using System.ComponentModel.DataAnnotations;

namespace Patuvai.Models.Enums
{
    public enum RequestListingStatus
    {
        [Display(Name = "Налично")]
        AVAILABLE,
        [Display(Name = "Транспортът е намерен")]
        TRANSPORT_FOUND,
        [Display(Name = "Не е нужен транспорт вече")]
        NO_LONGER_NEEDED,
        [Display(Name = "Затворен от администратор")]
        CLOSED_BY_ADMIN
    }
}
