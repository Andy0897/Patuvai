using System.ComponentModel.DataAnnotations;

namespace Patuvai.Models.Enums
{
    public enum TransportServiceType
    {
        [Display(Name = "Пътници")]
        PASSENGERS,
        [Display(Name = "Багаж/Товар")]
        LUGGAGE,
        [Display(Name = "Пътници и багаж")]
        PASSENGERS_AND_LUGGAGE
    }
}
