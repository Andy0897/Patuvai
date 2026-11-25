using Patuvai.Models;

namespace Patuvai.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review?> GetByIdAsync(int id);

        Task<List<Review>> GetAllAsync();

        Task AddAsync(Review review);

        Task UpdateAsync(Review review);

        Task DeleteAsync(int id);

        Task<bool> IsReviewedByUserAsync(int userId, int driverId);
    }
}
