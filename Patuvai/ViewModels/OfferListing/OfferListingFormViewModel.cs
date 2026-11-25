using Patuvai.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Patuvai.ViewModels.OfferListing
{
    public class OfferListingFormViewModel
    {
        [Required]
        public TripFrequency Frequency { get; set; }

        public DateOnly? TripDate { get; set; }

        public List<DayOfWeek> RecurringDays { get; set; } = new();

        public bool LuggageTransport { get; set; }

        public bool PassengerTransport { get; set; }

        public List<int> RouteCityIds { get; set; } = new();
    }
}