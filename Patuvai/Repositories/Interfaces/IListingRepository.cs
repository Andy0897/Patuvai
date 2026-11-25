using Patuvai.Models;

namespace Patuvai.Repositories.Interfaces
{
    public interface IListingRepository
    {
        Task<Listing?> GetByIdAsync(int id);

        Task<List<Listing>> GetAllAsync();
    }
}
