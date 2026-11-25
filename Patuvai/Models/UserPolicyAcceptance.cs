namespace Patuvai.Models
{
    public class UserPolicyAcceptance
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public ApplicationUser User { get; set; } = null!;

        public int PolicyId { get; set; }

        public Policy Policy { get; set; } = null!;

        public DateTime AcceptedAt { get; set; }

        public string IpAddress { get; set; } = null!;

        public string UserAgent { get; set; } = null!;
    }
}