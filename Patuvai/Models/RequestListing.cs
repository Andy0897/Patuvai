using Patuvai.Models.Enums;

namespace Patuvai.Models
{
    public class RequestListing : Listing
    {
        public DateOnly RequestDate { get; set; }

        public TransportServiceType TransportServiceType { get; set; }

        public string Description { get; set; } = string.Empty;

        public double Budget { get; set; }

        public int FromCityId { get; set; }

        public City FromCity { get; set; } = null!;

        public int ToCityId { get; set; }

        public City ToCity { get; set; } = null!;

        public RequestListingStatus Status { get; set; } = RequestListingStatus.AVAILABLE;
    }
}