using Patuvai.Models;
using Patuvai.ViewModels.Driver;

namespace Patuvai.Services.Interfaces
{
    public interface IDriverService
    {
        Task CreateAsync(DriverFormViewModel model);

        Task<DriverDetailsViewModel?> GetDriverDetailsAsync(int driverId);

        Task AddReviewAsync(Review review);

        Task<List<Driver>> GetAllAsync();

        Task<Driver?> GetByIdAsync(int id);
    }
}
