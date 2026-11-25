using Patuvai.ViewModels.Review;

namespace Patuvai.Services.Interfaces
{
    public interface IReviewService
    {
        Task CreateAsync(ReviewFormViewModel model);

        Task<bool> IsReviewedByUserAsync(int userId, int driverId);
    }
}
