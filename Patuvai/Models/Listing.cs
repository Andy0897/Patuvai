namespace Patuvai.Models
{
    public abstract class Listing
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public int PublisherId { get; set; }

        public ApplicationUser Publisher { get; set; } = null!;

        public ICollection<Conversation> Conversations { get; set; }
            = new List<Conversation>();
    }
}
