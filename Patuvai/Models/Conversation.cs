namespace Patuvai.Models
{
    public class Conversation
    {
        public int Id { get; set; }

        public int ListingId { get; set; }

        public Listing Listing { get; set; } = null!;

        public int ListingOwnerId { get; set; }

        public ApplicationUser ListingOwner { get; set; } = null!;

        public int ListingUserId { get; set; }

        public ApplicationUser ListingUser { get; set; } = null!;

        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
