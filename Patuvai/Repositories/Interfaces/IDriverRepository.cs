using Patuvai.Models;

namespace Patuvai.Repositories.Interfaces
{
    public interface IDriverRepository
    {
        Task<Driver?> GetByIdAsync(int id);

        Task<List<Driver>> GetAllAsync();

        Task AddAsync(Driver driver);

        Task UpdateAsync(Driver driver);

        Task DeleteAsync(int id);
    }
}
