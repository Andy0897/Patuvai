namespace Patuvai.Models
{
    public class RecurringTripDay
    {
        public int Id { get; set; }

        public int OfferListingId { get; set; }
        public OfferListing OfferListing { get; set; } = null!;

        public DayOfWeek DayOfWeek { get; set; }
    }
}
