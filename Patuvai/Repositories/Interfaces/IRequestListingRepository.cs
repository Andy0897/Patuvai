using Patuvai.Models;

namespace Patuvai.Repositories.Interfaces
{
    public interface IRequestListingRepository
    {
        Task<RequestListing?> GetByIdAsync(int id);

        Task<List<RequestListing>> GetAllAsync();

        Task AddAsync(RequestListing requestListing);

        Task UpdateAsync(RequestListing requestListing);

        Task DeleteAsync(int id);
    }
}
