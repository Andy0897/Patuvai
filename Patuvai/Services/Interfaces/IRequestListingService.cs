using Patuvai.Models;
using Patuvai.ViewModels.RequestListing;

namespace Patuvai.Services.Interfaces
{
    public interface IRequestListingService
    {
        Task CreateAsync(RequestListingFormViewModel model);

        Task UpdateAsync(RequestListingEditFormViewModel model);

        Task<List<RequestListing>> GetAllAsync();

        Task<RequestListing?> GetByIdAsync(int id);
    }
}