using Patuvai.Models;

namespace Patuvai.ViewModels.Conversation
{
    public class ConversationDetailsViewModel
    {
        public int ConversationId { get; set; }

        public Listing Listing { get; set; } = null!;

        public ApplicationUser ListingOwner { get; set; } = null!;

        public int ListingUserId { get; set; }

        public ApplicationUser ListingUser { get; set; } = null!;

        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
