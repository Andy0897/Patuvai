using Patuvai.Models.Enums;

namespace Patuvai.Models
{
    public class Policy
    {
        public int Id { get; set; }

        public PolicyType Type { get; set; }

        public double Version { get; set; }

        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }
    }
}
