using Patuvai.Models;
using Patuvai.Models.Enums;

namespace Patuvai.ViewModels.RequestListing
{
    public class RequestListingDetailsViewModel
    {
        public int Id { get; set; }

        public DateOnly RequestDate { get; set; }

        public TransportServiceType TransportServiceType { get; set; }

        public string Description { get; set; } = string.Empty;

        public double Budget { get; set; }

        public int FromCityId { get; set; }

        public City FromCity { get; set; } = null!;

        public int ToCityId { get; set; }

        public City ToCity { get; set; } = null!;

        public int PublisherId { get; set; }

        public ApplicationUser Publisher { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public bool isOwner { get; set; }
    }
}
