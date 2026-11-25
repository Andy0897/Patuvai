namespace Patuvai.ViewModels.Driver
{
    public class DriverDetailsViewModel
    {
        public int DriverId { get; set; }

        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Introduction { get; set; } = null!;

        public double AverageRating { get; set; }

        public int ReviewsCount { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool CanAddReview { get; set; }
    }
}
