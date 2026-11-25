namespace Patuvai.Models
{
    // Ensure the FK property name and type match the principal key on Conversation (assumed int)
    public class Message
    {
        public int Id { get; set; }

        public int ConversationId { get; set; }
        public Conversation Conversation { get; set; } = null!;

        public int SenderId { get; set; }
        public ApplicationUser Sender { get; set; } = null!;

        public string Content { get; set; } = null!;
        public DateTime SentAt { get; set; }
    }
}
