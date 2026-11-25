using System.Security.Claims;
using Patuvai.Models;
using Patuvai.Repositories.Interfaces;
using Patuvai.Services.Interfaces;
using Patuvai.ViewModels.Review;

namespace Patuvai.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IDriverService _driverService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReviewService(IReviewRepository reviewRepository, IDriverService driverService, IHttpContextAccessor httpContextAccessor)
        {
            _reviewRepository = reviewRepository;
            _driverService = driverService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateAsync(ReviewFormViewModel model)
        {
            string currentUserId = _httpContextAccessor.HttpContext?.User?
            .FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("User is not logged in.");

            Review review = new()
            {
                ReviewerId = int.Parse(currentUserId),
                DriverId = model.DriverId,
                Rating = model.Rating,
                Comment = model.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _driverService.AddReviewAsync(review);
        }

        public async Task<bool> IsReviewedByUserAsync(int userId, int driverId)
        {
            return await _reviewRepository.IsReviewedByUserAsync(userId, driverId);
        }
    }
}
