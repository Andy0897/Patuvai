namespace Patuvai.Models
{
    public class RouteStop
    {
        public int Id { get; set; }

        public int OfferListingId { get; set; }
        public OfferListing OfferListing { get; set; } = null!;

        public int CityId { get; set; }
        public City City { get; set; } = null!;

        public int Order { get; set; }
    }
}
