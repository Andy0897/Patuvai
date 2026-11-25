namespace Patuvai.Models
{
    public class Driver
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public ApplicationUser User { get; set; } = null!;

        //TODO: Да се добави снимка на шофьора

        public string Introduction { get; set; } = null!;

        public double AverageRating { get; set; }

        public int ReviewsCount { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}