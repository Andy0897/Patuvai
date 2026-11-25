using Patuvai.Models.Enums;

namespace Patuvai.Models
{
    public class OfferListing : Listing
    {
        public TripFrequency Frequency { get; set; }

        public DateOnly? TripDate { get; set; }

        public ICollection<RecurringTripDay> RecurringDays { get; set; }
            = new List<RecurringTripDay>();

        public bool LuggageTransport { get; set; }

        public bool PassengerTransport { get; set; }

        public OfferListingStatus Status { get; set; }
            = OfferListingStatus.AVAILABLE;

        public ICollection<RouteStop> RouteStops { get; set; }
            = new List<RouteStop>();
    }
}
