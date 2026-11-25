namespace Patuvai.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int ReviewerId { get; set; }

        public ApplicationUser Reviewer { get; set; } = null!;

        public int DriverId { get; set; }

        public Driver Driver { get; set; } = null!;

        public int Rating { get; set; }

        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}